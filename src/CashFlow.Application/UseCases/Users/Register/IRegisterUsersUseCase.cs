namespace CashFlow.Application.UseCases.Users.Register;

public interface IRegisterUsersUseCase
{
    Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}