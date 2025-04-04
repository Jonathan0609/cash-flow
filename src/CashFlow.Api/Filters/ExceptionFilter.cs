using CashFlow.Communication.Errors;
using CashFlow.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
            HandleProjectException(context);
        else
            ThrowUnknownError(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var cashFlowException = context.Exception as CashFlowException;
        var errorResponse = new ErrorResponse(cashFlowException!.GetErrors());
        
        context.HttpContext.Response.StatusCode = cashFlowException!.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    } 
    
    private void ThrowUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        context.Result = new ObjectResult(new ErrorResponse(ResourceErrorMessages.UNKNOWN_ERROR));
    }
}