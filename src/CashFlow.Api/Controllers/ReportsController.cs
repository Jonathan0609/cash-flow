using System.Net.Mime;
using CashFlow.Application.UseCases.Reports.Expenses.Excel;
using CashFlow.Application.UseCases.Reports.Expenses.Pdf;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReportsController : ControllerBase
{
    [HttpGet("excel/expenses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IReportExpensesExcelUseCase useCase,
        [FromQuery] ReportExcelExpensesRequest request)
    {
        var file = await useCase.Execute(request);
        
        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "Expenses.xlsx");
 
        return NoContent();
    }
    
    [HttpGet("pdf/expenses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IReportExpensesPdfUseCase useCase,
        [FromQuery] ReportPdfExpenseRequest request)
    {
        var file = await useCase.Execute(request);
        
        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
 
        return NoContent();
    }
}