using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUsersValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUsersValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress()
            .When(user => !string.IsNullOrWhiteSpace(user.Email), ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
        
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());
    }
}