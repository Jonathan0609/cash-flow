using CashFlow.Application.UseCases._Enums;
using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.Expenses;
using Shouldly;

namespace Validators.Tests.Expenses.Create;

public class CreateExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange

        var validator = new CreateExpenseValidator();

        var request = CreateExpenseRequestBuilder.Build();

        //Act
        
        var result = validator.Validate(request);

        //Assert 
        
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ErrorTitleEmpty(string title)
    {
        //Arrange
        var validator = new CreateExpenseValidator();
        var request = CreateExpenseRequestBuilder.Build();
        
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Asserts
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.TITLE_REQUIRED));
    }
    
    [Fact]
    public void ErrorPaymentTypeInvalid()
    {
        //Arrange
        var validator = new CreateExpenseValidator();

        var request = CreateExpenseRequestBuilder.Build();
        
        request.PaymentType = (PaymentTypeEnum)700;

        //Act
        var result = validator.Validate(request);

        //Asserts
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ErrorAmountInvalid(decimal amount)
    {
        //Arrange
        var validator = new CreateExpenseValidator();

        var request = CreateExpenseRequestBuilder.Build();
        
        request.Value = amount;

        //Act
        var result = validator.Validate(request);

        //Asserts
        result.IsValid.ShouldBeFalse();
        
        result.Errors.ShouldSatisfyAllConditions(
            () => result.Errors.ShouldHaveSingleItem(), 
            () => result.Errors.ShouldContain(error => error.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }
}