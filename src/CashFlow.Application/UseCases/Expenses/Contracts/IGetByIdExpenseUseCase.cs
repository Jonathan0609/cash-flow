using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Contracts;

public interface IGetByIdExpenseUseCase
{
    Task<GetByIdExpensesResponse> Execute(long id);
}