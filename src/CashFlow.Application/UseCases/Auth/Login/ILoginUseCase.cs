using CashFlow.Communication.Requests.Login;
using CashFlow.Communication.Responses.Users;

namespace CashFlow.Application.UseCases.Auth.Login;

public interface ILoginUseCase
{
    Task<RegisterUserResponse> Execute(LoginRequest request);
}