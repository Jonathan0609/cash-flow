using CashFlow.Communication.Enums;

namespace CashFlow.Communication.Requests.Expenses;

public class UpdateExpenseRequest
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal Value { get; set; }
    
    public PaymentTypeEnum PaymentType { get; set; }
}