using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class MaxItemQuantitySpecification : BaseSpecification<SaleItem>
{
    private const int MaxQuantity = 20;

    public MaxItemQuantitySpecification()
        : base(item => item.Quantity <= MaxQuantity)
    {
    }

    public static int GetMaxQuantity() => MaxQuantity;
}