﻿using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase: IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(IExpensesWriteOnlyRepository repository,  IUnitOfWork unitOfWork)
    {
        _expensesRepository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _expensesRepository.Delete(id);

        if (!result)
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        
        await _unitOfWork.Commit();
    }
}