namespace CashFlow.Application.UseCases.Expenses.Create;

public interface ICreateExpenseUseCase
{
    Task<CreateExpensesResponse> Execute(CreateExpensesRequest request);
}