using CashFlow.Communication.Requests.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Contracts;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}