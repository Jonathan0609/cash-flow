using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Configurations.AutoMapper;

public class AutoMapping: Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<CreateExpensesRequest, Expense>();
        CreateMap<UpdateExpenseRequest, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, CreateExpensesResponse>();
        CreateMap<Expense, ExpensesResponse>();
        CreateMap<Expense, GetByIdExpensesResponse>();
    }
}