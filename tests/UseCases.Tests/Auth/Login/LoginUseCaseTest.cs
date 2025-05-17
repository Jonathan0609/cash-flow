using CashFlow.Application.UseCases.Auth.Login;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.Auth;
using CommonTestUtilities.Token;
using Shouldly;

namespace UseCases.Tests.Auth.Login;

public class LoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        
        var request = LoginRequestBuilder.Build();
        request.Email = user.Email;
        
        var useCase = CreateUseCase(user, request.Password);
        
        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        
        result.Token.ShouldNotBeNullOrWhiteSpace();
    }
    
    private LoginUseCase CreateUseCase(User user, string password)
    {
        var passwordEncripter = new PasswordEncripterBuilder();
        var accessTokenGenerator = AccessTokenGeneratorBuilder.Build();
        var readOnlyRepository = new UserReadOnlyRepositoryBuilder();
        
        readOnlyRepository.GetUserByEmail(user);
        
        passwordEncripter.VerifyPassword(password);
        
        return new LoginUseCase(passwordEncripter.Build(), readOnlyRepository.Build(), accessTokenGenerator);
    }
}