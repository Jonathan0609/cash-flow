using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Users;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCases.Tests.Users.Register;

public class RegisterUsersUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        
        result.Token.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ErrorNameEmpty()
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        
        var act = async () => await useCase.Execute(request);
       
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldSatisfyAllConditions(
            () => result.GetErrors().ShouldHaveSingleItem(), 
            () => result.GetErrors().ShouldContain(error => error == ResourceErrorMessages.NAME_REQUIRED));
    }

    [Fact]
    public async Task ErrorEmailAlreadyExist()
    {
        var request = RegisterUserRequestBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);
       
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();

        result.GetErrors().ShouldSatisfyAllConditions(
            () => result.GetErrors().ShouldHaveSingleItem(), 
            () => result.GetErrors().ShouldContain(error => error == ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUsersUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = new PasswordEncripterBuilder();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();

        if (!string.IsNullOrWhiteSpace(email))
            readOnlyRepository.ExistActiveUserWithEmail(email);
        
        return new RegisterUsersUseCase(
            mapper,
            passwordEncripter.Build(),
            readOnlyRepository.Build(),
            writeOnlyRepository,
            unitOfWork,
            accessTokenGenerator);
    }
}