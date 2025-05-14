using AutoMapper;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Users;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUsersUseCase : IRegisterUsersUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    
    public RegisterUsersUseCase(
        IMapper mapper,
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);
        
        var user = _mapper.Map<User>(request);
        
        user.Password = _passwordEncripter.EncryptPassword(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        
        await _userWriteOnlyRepository.Register(user);
        
        await _unitOfWork.Commit();

        return new RegisterUserResponse
        {
            Token = _accessTokenGenerator.GenerateAccessToken(user)
        };
    }
    
    private async Task Validate(RegisterUserRequest request)
    {
        var result = new RegisterUsersValidator().Validate(request);
        
        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(
                string.Empty,
                ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }
            

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}