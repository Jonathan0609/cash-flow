namespace CashFlow.Application.UseCases.Reports.Expenses.Excel;

public interface IReportExpensesExcelUseCase
{
    public Task<byte[]> Execute(ReportExcelExpensesRequest request);
}