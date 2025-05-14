using AutoMapper;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Requests.Users;
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
        #region Expenses

        CreateMap<CreateExpensesRequest, Expense>();
        CreateMap<UpdateExpenseRequest, Expense>();

        #endregion

        #region Users

        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        #endregion
  
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, CreateExpensesResponse>();
        CreateMap<Expense, ExpensesResponse>();
        CreateMap<Expense, GetByIdExpensesResponse>();
    }
}