﻿namespace CashFlow.Application.UseCases.Expenses.List;

public interface IListExpenseUseCase
{
    Task<ListExpensesResponse> Execute();
}