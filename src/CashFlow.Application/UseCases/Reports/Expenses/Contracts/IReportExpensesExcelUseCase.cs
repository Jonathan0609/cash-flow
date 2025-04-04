using CashFlow.Communication.Requests.Reports.Expenses;

namespace CashFlow.Application.UseCases.Reports.Expenses.Contracts;

public interface IReportExpensesExcelUseCase
{
    public Task<byte[]> Execute(ReportExcelExpensesRequest request);
}