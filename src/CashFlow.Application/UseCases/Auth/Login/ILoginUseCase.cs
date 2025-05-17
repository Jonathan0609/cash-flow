using CashFlow.Application.UseCases.Users.Register;

namespace CashFlow.Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<RegisterUserResponse> Execute(LoginRequest request);
}