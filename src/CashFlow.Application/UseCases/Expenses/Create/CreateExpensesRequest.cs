using System.Text.Json.Serialization;
using CashFlow.Application.UseCases._Enums;

namespace CashFlow.Application.UseCases.Expenses.Create;

public class CreateExpensesRequest
{
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    [JsonIgnore]
    public DateTime Date { get; set; } = DateTime.Now;
    
    public decimal Value { get; set; }
    
    public PaymentTypeEnum PaymentType { get; set; }
}