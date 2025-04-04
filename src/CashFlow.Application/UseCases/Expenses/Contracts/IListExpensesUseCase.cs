using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Contracts;

public interface IListExpenseUseCase
{
    Task<ListExpensesResponse> Execute();
}