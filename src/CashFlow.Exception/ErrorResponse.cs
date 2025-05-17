namespace CashFlow.Exception;

public class ErrorResponse
{
    public List<string> Message { get; set; }

    public ErrorResponse(string message)
    {
        Message = [message];
    }

    public ErrorResponse(List<string> errorMessages)
    {
        Message = errorMessages;
    }
}