using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Extensions;

public static class PaymentTypeExtensions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de crédito",
            PaymentType.Pix => "Pix",
            PaymentType.DebitCard => "Cartão de débito",
            _ => string.Empty,
        };
    }
}