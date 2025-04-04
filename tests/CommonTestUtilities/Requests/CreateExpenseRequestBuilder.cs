using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests.Expenses;

namespace CommonTestUtilities.Requests;

// https://github.com/bchavez/Bogus?tab=readme-ov-file
public class CreateExpenseRequestBuilder
{
    public static CreateExpensesRequest Build()
    {
        return new Faker<CreateExpensesRequest>()
            .RuleFor(request => request.Title, faker => faker.Commerce.ProductName())
            .RuleFor(request => request.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(request => request.Date, faker => faker.Date.Past())
            .RuleFor(request => request.PaymentType, faker => faker.PickRandom<PaymentTypeEnum>())
            .RuleFor(request => request.Value, faker => faker.Random.Decimal(min: 1, max: 1000));
    }
}