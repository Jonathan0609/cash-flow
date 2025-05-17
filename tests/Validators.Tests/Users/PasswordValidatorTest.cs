using CashFlow.Application.UseCases.Users;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using FluentValidation;
using Shouldly;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaa1")]
    public void ErrorPasswordInvalid(string password)
    {
        //Arrange
        var validator = new PasswordValidator<RegisterUserRequest>();
        
        //Act
        var result = validator
            .IsValid(new ValidationContext<RegisterUserRequest>(new RegisterUserRequest()), password);
        
        //Assert
        result.ShouldBeFalse();
    }
}