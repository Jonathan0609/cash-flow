using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests.Users;
using Shouldly;

namespace Validators.Tests.Users.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterUsersValidator();
        var request = RegisterUserRequestBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void ErrorNameEmpty(string name)
    {
        //Arrange
        var validator = new RegisterUsersValidator();
        var request = RegisterUserRequestBuilder.Build();

        request.Name = name;
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.NAME_REQUIRED));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void ErrorEmailEmpty(string email)
    {
        //Arrange
        var validator = new RegisterUsersValidator();
            
        var request = RegisterUserRequestBuilder.Build();
        request.Email = email;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.EMAIL_REQUIRED));
    }
    
    [Fact]
    public void ErrorEmailInvalid()
    {
        //Arrange
        var validator = new RegisterUsersValidator();
            
        var request = RegisterUserRequestBuilder.Build();
        request.Email = "Jonathan.com";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.EMAIL_INVALID));
    }
    
    [Fact]
    public void ErrorPasswordEmpty()
    {
        //Arrange
        var validator = new RegisterUsersValidator();
            
        var request = RegisterUserRequestBuilder.Build();
        request.Password = string.Empty;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.INVALID_PASSWORD));
    }
}