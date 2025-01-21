using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class MaxItemQuantitySpecificationTests
{
    [Fact(DisplayName = "Max item quantity should be 20")]
    public void MaxItemQuantity_ShouldBe20()
    {
        Assert.Equal(20, MaxItemQuantitySpecification.GetMaxQuantity());
        Assert.True(new MaxItemQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(20)));
        Assert.Throws<InvalidQuantityException>(() => new MaxItemQuantitySpecification().IsSatisfiedBy(SaleItemTestData.GetItemWithQuantity(21)));
    }

}