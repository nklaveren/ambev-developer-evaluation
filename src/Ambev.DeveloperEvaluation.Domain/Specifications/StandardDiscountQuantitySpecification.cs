using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class StandardDiscountQuantitySpecification : BaseSpecification<SaleItem>
{
    private const int MinQuantityForStandardDiscount = 4;
    private const int MaxQuantityForStandardDiscount = 9;
    public const decimal StandardDiscountPercentage = 0.10m;

    public StandardDiscountQuantitySpecification()
        : base(item => item.Quantity >= MinQuantityForStandardDiscount && item.Quantity <= MaxQuantityForStandardDiscount)
    {
    }

    public static int GetMinQuantity() => MinQuantityForStandardDiscount;
    public static int GetMaxQuantity() => MaxQuantityForStandardDiscount;
}