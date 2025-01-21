using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class MinItemQuantityForDiscountSpecificationTests
{
    [Fact(DisplayName = "Min item quantity for discount should be 4")]
    public void MinItemQuantityForDiscount_ShouldBe4()
    {
        Assert.Equal(4, MinItemQuantityForDiscountSpecification.GetMinQuantityForDiscount());
        Assert.True(new MinItemQuantityForDiscountSpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(4)));
        Assert.False(new MinItemQuantityForDiscountSpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(3)));
    }
}