using AutoMapper;
using CashFlow.Application.UseCases.Expenses;
using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Users.Register;
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