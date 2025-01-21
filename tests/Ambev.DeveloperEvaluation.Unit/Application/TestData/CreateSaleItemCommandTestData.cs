using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleItemCommandTestData
{
    private static readonly Faker _faker = new("pt_BR");

    public static CreateSaleItemCommand CreateItemValid() => new()
    {
        ProductId = _faker.Random.Guid(),
        Quantity = _faker.Random.Int(1, 20),
        UnitPrice = _faker.Random.Decimal(1, 100)
    };
}
