using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Communication.Errors;
using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUsersUseCase useCase, [FromBody] RegisterUserRequest request)
    {
        var response = await useCase.Execute(request);
        
        return Created(string.Empty, response);
    }
}