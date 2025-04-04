using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infra.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.Repositories;

internal class ExpensesRepository: IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpenseUpdateOnlyRepository
{
    private readonly CashFlowDbContext _context;
    
    public ExpensesRepository(CashFlowDbContext context)
    {
        _context = context;
    }
    public async Task Add(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> List()
    {
        return await _context.Expenses
            .AsNoTracking()
            .ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await _context.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Expense>> GetExpensesByMonth(DateOnly date)
    {
       return await _context.Expenses
            .AsNoTracking()
            .Where(expense => expense.Date.Month == date.Month &&
                              expense.Date.Year == date.Year)
            .OrderBy(expense => expense.Date)
            .ThenBy(expense => expense.Title)
            .ToListAsync();
    }

    async Task<Expense?> IExpenseUpdateOnlyRepository.GetById(long id)
    {
        return await _context.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> Delete(long id)
    {
        var expense = await _context.Expenses
            .FirstOrDefaultAsync(expense => expense.Id == id);
        
        if (expense is null)
            return false;
        
        _context.Expenses.Remove(expense);

        return true;
    }

    public void Update(Expense expense)
    {
        _context.Expenses.Update(expense);
    }
}