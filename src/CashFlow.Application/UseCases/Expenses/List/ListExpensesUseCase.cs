using AutoMapper;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.List;

public class ListExpensesUseCase: IListExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository  _expensesRepository;
    private readonly IMapper _mapper;
    
    public ListExpensesUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _mapper = mapper;
    }
    public async Task<ListExpensesResponse> Execute()
    {
        var result = await _expensesRepository.List();

        return new ListExpensesResponse
        {
            Data = _mapper.Map<List<ExpensesResponse>>(result)
        };
    }
}