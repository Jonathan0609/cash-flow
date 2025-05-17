using CashFlow.Application.UseCases._Enums;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetByIdExpensesResponse
{
    public long Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime Date { get; set; }
    
    public decimal Value { get; set; }
    
    public PaymentTypeEnum PaymentType { get; set; }
}