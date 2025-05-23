﻿using Bogus;
using CashFlow.Application.UseCases._Enums;
using CashFlow.Application.UseCases.Expenses.Create;

namespace CommonTestUtilities.Requests.Expenses;

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