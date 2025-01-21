using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class HighDiscountQuantitySpecificationTests
{
    [Fact(DisplayName = "High discount quantity should be between 10 and 20")]
    public void HighDiscountQuantity_ShouldBeBetween10And20()
    {
        Assert.Equal(10, HighDiscountQuantitySpecification.GetMinQuantity());
        Assert.Equal(20, HighDiscountQuantitySpecification.GetMaxQuantity());
        var item = SaleItemTestData.GetItemWithQuantity(10);
        Assert.True(new HighDiscountQuantitySpecification().IsSatisfiedBy(item));
        Assert.Equal(HighDiscountQuantitySpecification.HighDiscountPercentage, item.Discount.Percentage);
        item = SaleItemTestData.GetItemWithQuantity(20);
        Assert.True(new HighDiscountQuantitySpecification().IsSatisfiedBy(item));
        Assert.Equal(HighDiscountQuantitySpecification.HighDiscountPercentage, item.Discount.Percentage);
        Assert.False(new HighDiscountQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(9)));
        Assert.Equal(StandardDiscountQuantitySpecification.StandardDiscountPercentage, SaleItemTestData.GetItemWithQuantity(9).Discount.Percentage);
    }
}