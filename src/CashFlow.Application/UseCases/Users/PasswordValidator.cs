using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using MigraDoc.DocumentObjectModel;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PasswordValidator";

    private const string ErrorMessageKey = "ErrorMessage";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ErrorMessageKey}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (password.IsValueNullOrEmpty())
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        if (!UpperCaseLetter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        
        if (!LowerCaseLetter().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        
        if (!Numbers().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        
        if (!SpecialSymbols().IsMatch(password))
        {
            context.MessageFormatter.AppendArgument(ErrorMessageKey, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }
        
        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
    
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseLetter();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Numbers();
    
    [GeneratedRegex(@"[\!\?\*\.]+")]
    private static partial Regex SpecialSymbols();
}