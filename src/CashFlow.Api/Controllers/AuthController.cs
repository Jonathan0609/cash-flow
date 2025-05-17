using CashFlow.Application.UseCases.Auth.Login;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] LoginRequest request)
    {
        var result = await useCase.Execute(request);
        
        return Ok(result);
    }
}