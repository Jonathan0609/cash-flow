using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Auth.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    
    public LoginUseCase(
        IPasswordEncripter passwordEncripter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }
    
    public async Task<RegisterUserResponse> Execute(LoginRequest request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if (user is null)
            throw new InvalidLoginException();
        
        var passwordMatch = _passwordEncripter.VerifyPassword(request.Password, user.Password);
        
        if(passwordMatch is false)
            throw new InvalidLoginException();

        return new RegisterUserResponse
        {
            Token = _accessTokenGenerator.GenerateAccessToken(user)
        };
    }
}