using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Create;

public interface ICreateExpenseUseCase
{
    Task<CreateExpensesResponse> Execute(CreateExpensesRequest request);
}