using CashFlow.Application.Configurations.AutoMapper;
using CashFlow.Application.UseCases.Auth.Login;
using CashFlow.Application.UseCases.Expenses.Create;
using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.List;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Application.UseCases.Reports.Expenses.Contracts;
using CashFlow.Application.UseCases.Reports.Expenses.Excel;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf;
using CashFlow.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application.Configurations;

public static class DependencyInjectionExtension
{
    public static void AddApplicationInjections(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
    
    private static void AddUseCases(this IServiceCollection services)
    {
        #region Expenses

        services.AddScoped<ICreateExpenseUseCase, CreateExpensesUseCase>();
        services.AddScoped<IListExpenseUseCase, ListExpensesUseCase>();
        services.AddScoped<IGetByIdExpenseUseCase, GetByIdExpenseUseCase>();
        services.AddScoped<IDeleteExpenseUseCase, DeleteExpenseUseCase>();
        services.AddScoped<IUpdateExpenseUseCase, UpdateExpenseUseCase>();

        #endregion
        
        #region Report Expenses
        
        services.AddScoped<IReportExpensesExcelUseCase, ReportExpensesExcelUseCase>();
        services.AddScoped<IReportExpensesPdfUseCase, ReportExpensesPdfUseCase>();
        #endregion

        #region Users

        services.AddScoped<IRegisterUsersUseCase, RegisterUsersUseCase>();

        #endregion

        #region Auth

        services.AddScoped<ILoginUseCase, LoginUseCase>();

        #endregion
    }
}