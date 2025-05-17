namespace CashFlow.Application.UseCases.Reports.Expenses.Pdf;

public interface IReportExpensesPdfUseCase
{
    public Task<byte[]> Execute(ReportPdfExpenseRequest request);
}