using CashFlow.Communication.Responses.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public interface IGetByIdExpenseUseCase
{
    Task<GetByIdExpensesResponse> Execute(long id);
}