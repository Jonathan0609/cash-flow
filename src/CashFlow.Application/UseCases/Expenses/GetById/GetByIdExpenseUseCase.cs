using AutoMapper;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetByIdExpenseUseCase: IGetByIdExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesRepository;
    private readonly IMapper _mapper;
    
    public GetByIdExpenseUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task<GetByIdExpensesResponse> Execute(long id)
    {
        var result = await _expensesRepository.GetById(id);

        if (result is null)
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        
        return _mapper.Map<GetByIdExpensesResponse>(result);
    }
}