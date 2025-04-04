using CashFlow.Communication.Requests.Reports.Expenses;

namespace CashFlow.Application.UseCases.Reports.Expenses.Contracts;

public interface IReportExpensesPdfUseCase
{
    public Task<byte[]> Execute(ReportPdfExpenseRequest request);
}