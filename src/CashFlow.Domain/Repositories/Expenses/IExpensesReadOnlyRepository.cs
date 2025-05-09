﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesReadOnlyRepository
{
    Task<List<Expense>> List();
     
    Task<Expense?> GetById(long id);
    
    Task<List<Expense>> GetExpensesByMonth(DateOnly date);
}