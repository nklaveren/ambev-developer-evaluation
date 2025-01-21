using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class StandardDiscountQuantitySpecificationTests
{
    [Fact(DisplayName = "Standard discount quantity should be 4 to 9")]
    public void StandardDiscountQuantity_ShouldBe4To9()
    {
        Assert.Equal(4, StandardDiscountQuantitySpecification.GetMinQuantity());
        Assert.Equal(9, StandardDiscountQuantitySpecification.GetMaxQuantity());
        Assert.False(new StandardDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(3)));
        Assert.True(new StandardDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(5)));
        Assert.False(new StandardDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(10)));
        Assert.True(new StandardDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(4)));
        Assert.True(new StandardDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(9)));
    }
}