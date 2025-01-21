using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class MinItemQuantityForDiscountSpecification : BaseSpecification<SaleItem>
{
    private const int MinQuantityForDiscount = 4;

    public MinItemQuantityForDiscountSpecification()
        : base(item => item.Quantity >= MinQuantityForDiscount)
    {
    }

    public static int GetMinQuantityForDiscount() => MinQuantityForDiscount;
}