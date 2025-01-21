using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    private static readonly Faker _faker = new("pt_BR");

    /// <summary>
    /// Get a valid SaleItem
    /// Max quantity is 20
    /// </summary>
    /// <returns>A valid SaleItem</returns>
    public static SaleItem GetValidItem(Guid saleId)
        => new(Guid.NewGuid(), saleId, Guid.NewGuid(), _faker.Random.Int(1, 20), _faker.Random.Decimal(1, 100));

    /// <summary>
    /// Get a SaleItem with a specific quantity
    /// </summary>
    /// <param name="quantity">The quantity of the item</param>
    /// <returns>A SaleItem with the specified quantity</returns>
    public static SaleItem GetItemWithQuantity(int quantity)
        => new(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), quantity, _faker.Random.Decimal(1, 100));

    public static SaleItem GetItemWithInvalidProduct(Guid productId)
        => new(Guid.NewGuid(), Guid.Empty, productId, _faker.Random.Int(1, 20), _faker.Random.Decimal(1, 100));

    /// <summary>
    /// Get a list of valid SaleItems
    /// Max quantity is 20
    /// </summary>
    /// <param name="quantity">The quantity of items to generate</param>
    /// <returns>A list of valid SaleItems</returns>
    public static List<SaleItem> GetSaleItems(int quantity, Guid saleId)
        => [.. Enumerable.Range(0, quantity).Select(_ => GetValidItem(saleId))];

    public static SaleItem GetItemWithUnitPrice(decimal unitPrice)
        => new(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), _faker.Random.Int(1, 20), unitPrice);

    public static SaleItem GetItemWithZeroDiscount()
        => new(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), _faker.Random.Int(1, 3), _faker.Random.Decimal(1, 100));
}
