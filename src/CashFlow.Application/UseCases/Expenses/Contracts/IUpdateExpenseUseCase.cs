using CashFlow.Communication.Requests.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Contracts;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, UpdateExpenseRequest request);
}