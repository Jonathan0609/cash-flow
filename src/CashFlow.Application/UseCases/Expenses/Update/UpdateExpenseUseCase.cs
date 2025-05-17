using AutoMapper;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExpenseUpdateOnlyRepository _expensesRepository;
    private readonly IMapper _mapper;
    
    public UpdateExpenseUseCase(
        IUnitOfWork unitOfWork,
        IExpenseUpdateOnlyRepository expensesRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task Execute(long id, UpdateExpenseRequest request)
    {
        Validate(request);
        
        var expense = await _expensesRepository.GetById(id);

        if (expense == null)
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        
        var expenseMap = _mapper.Map(request, expense);
        
        _expensesRepository.Update(expenseMap);
        
        await _unitOfWork.Commit();
    }
    
    private static void Validate(UpdateExpenseRequest request)
    {
        var validator = new UpdateExpenseValidator();
        
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();
            
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}