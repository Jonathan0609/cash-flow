namespace CashFlow.Communication.Responses.Expenses;

public class ExpensesResponse
{
    public long Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public decimal Value { get; set; }
}