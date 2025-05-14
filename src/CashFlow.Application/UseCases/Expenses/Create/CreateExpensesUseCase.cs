using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.Create;

internal class CreateExpensesUseCase : ICreateExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateExpensesUseCase(
        IExpensesWriteOnlyRepository expensesRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<CreateExpensesResponse> Execute(CreateExpensesRequest request)
    {
        Validate(request);
        
        var entity = _mapper.Map<Expense>(request);

        await _expensesRepository.Add(entity);
        
        await _unitOfWork.Commit();

        return _mapper.Map<CreateExpensesResponse>(entity);
    }

    private static void Validate(CreateExpensesRequest request)
    {
        var validator = new CreateExpenseValidator();
        
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