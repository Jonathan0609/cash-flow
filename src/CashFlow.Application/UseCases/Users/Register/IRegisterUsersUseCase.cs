using CashFlow.Communication.Requests.Users;
using CashFlow.Communication.Responses.Users;

namespace CashFlow.Application.UseCases.Users.Register;

public interface IRegisterUsersUseCase
{
    Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}