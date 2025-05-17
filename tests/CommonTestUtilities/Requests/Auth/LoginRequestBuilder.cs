using Bogus;
using CashFlow.Application.UseCases.Auth.Login;

namespace CommonTestUtilities.Requests.Auth;

public class LoginRequestBuilder
{
    public static LoginRequest Build()
    {
        return new Faker<LoginRequest>()
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}