using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class BaseSpecificationTests
{
    [Fact(DisplayName = "Base specification should be satisfied by an entity")]
    public void BaseSpecification_ShouldBeSatisfiedByAnEntity()
    {
        var item = new SaleItem(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(), 1, 1);
        var specification = new BaseSpecificationStub();
        Assert.True(specification.IsSatisfiedBy(item));
        Assert.NotNull(specification.ToExpression());
    }
}

public class BaseSpecificationStub : BaseSpecification<SaleItem>
{
    public BaseSpecificationStub() : base(item => item.Quantity > 0) { }
}
