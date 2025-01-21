using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class HighDiscountQuantitySpecification : BaseSpecification<SaleItem>
{
    private const int MinQuantityForHighDiscount = 10;
    private const int MaxQuantityForHighDiscount = 20;
    public const decimal HighDiscountPercentage = 0.20m;

    public HighDiscountQuantitySpecification()
        : base(item => item.Quantity >= MinQuantityForHighDiscount && item.Quantity <= MaxQuantityForHighDiscount)
    {
    }

    public static int GetMinQuantity() => MinQuantityForHighDiscount;
    public static int GetMaxQuantity() => MaxQuantityForHighDiscount;
}