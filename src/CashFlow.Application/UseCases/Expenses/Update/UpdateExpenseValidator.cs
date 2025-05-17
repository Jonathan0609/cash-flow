using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseValidator: AbstractValidator<UpdateExpenseRequest>
{
    public UpdateExpenseValidator()
    {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.TITLE_REQUIRED);
        
        RuleFor(expense => expense.Value)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        
        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }
}