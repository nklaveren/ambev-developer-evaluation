using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker _faker = new("pt_BR");

    public static Sale GetValidSaleWithItems(int quantity = 10, bool isCustomerIdValid = true, bool isBranchIdValid = true)
    {
        var customerId = isCustomerIdValid ? _faker.Random.Guid() : Guid.Empty;
        var branchId = isBranchIdValid ? _faker.Random.Guid() : Guid.Empty;
        var items = SaleItemTestData.GetSaleItems(quantity, Guid.Empty);
        return new Sale(Guid.Empty, customerId, branchId, items);
    }
}
