using CashFlow.Application.UseCases.Expenses.Contracts;
using CashFlow.Communication.Errors;
using CashFlow.Communication.Requests.Expenses;
using CashFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ExpensesController : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateExpensesResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromServices] ICreateExpenseUseCase useCase,
        [FromBody] CreateExpensesRequest request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ListExpensesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        [FromServices] IListExpenseUseCase useCase)
    {
        var result = await useCase.Execute();
        
        if(result.Data.Any())
            return Ok(result);
        
        return NoContent();
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdExpensesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetByIdExpenseUseCase useCase,
        [FromRoute] long id)
    {
        var result = await useCase.Execute(id);
        
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteExpenseUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);
        
        return NoContent();
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateExpenseUseCase useCase,
        [FromRoute] long id,
        [FromBody] UpdateExpenseRequest request)
    {
        await useCase.Execute(id, request);
        
        return NoContent();
    }
}