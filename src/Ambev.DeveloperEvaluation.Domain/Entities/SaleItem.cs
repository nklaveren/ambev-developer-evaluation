using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    protected SaleItem() { }

    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = Money.Zero();
    public Discount Discount { get; private set; } = Discount.None();
    public Money TotalAmount { get; private set; } = Money.Zero();
    public bool IsCanceled { get; private set; }

    public SaleItem(Guid id, Guid saleId, Guid productId, int quantity, decimal unitPrice)
    {
        ValidateQuantity(quantity);

        Id = id;
        SaleId = saleId;

        try
        {
            UnitPrice = new Money(unitPrice);
        }
        catch (DomainException)
        {
            throw InvalidMoneyException.NegativeAmount(unitPrice);
        }

        if (productId == Guid.Empty)
            throw InvalidProductException.InvalidId(productId, $"Product id is required in sale {SaleId}");

        ProductId = productId;
        Quantity = quantity;

        CalculateDiscount();
        CalculateTotalAmount();
    }

    private static void ValidateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw InvalidQuantityException.BelowMinimum(quantity, 1);

        var maxQuantitySpec = new MaxItemQuantitySpecification();
        if (!maxQuantitySpec.IsSatisfiedBy(new SaleItem { Quantity = quantity }))
            throw InvalidQuantityException.AboveMaximum(quantity, MaxItemQuantitySpecification.GetMaxQuantity());
    }

    public void Cancel()
    {
        if (IsCanceled)
            throw InvalidSaleStateException.AlreadyCancelled(SaleId);

        IsCanceled = true;
        AddDomainEvent(new SaleItemCanceledEvent(
            Id,
            SaleId,
            ProductId,
            Quantity,
            UnitPrice.Amount,
            TotalAmount.Amount));
    }

    private void CalculateDiscount()
    {
        var highDiscountSpec = new HighDiscountQuantitySpecification();
        var standardDiscountSpec = new StandardDiscountQuantitySpecification();
        var minQuantitySpec = new MinItemQuantityForDiscountSpecification();

        var baseAmount = UnitPrice.Multiply(Quantity);

        if (!minQuantitySpec.IsSatisfiedBy(this))
        {
            Discount = Discount.None(UnitPrice.Currency);
            return;
        }

        if (highDiscountSpec.IsSatisfiedBy(this))
            Discount = Discount.FromPercentage(
                HighDiscountQuantitySpecification.HighDiscountPercentage,
                baseAmount,
                $"High volume discount ({Quantity} items)");
        else if (standardDiscountSpec.IsSatisfiedBy(this))
            Discount = Discount.FromPercentage(
                StandardDiscountQuantitySpecification.StandardDiscountPercentage,
                baseAmount,
                $"Standard volume discount ({Quantity} items)");
        else
            Discount = Discount.None(UnitPrice.Currency);
    }

    private void CalculateTotalAmount()
    {
        var subtotal = UnitPrice.Multiply(Quantity);
        TotalAmount = Discount.ApplyTo(subtotal);
    }

    public void UpdateSaleId(Guid saleId)
    {
        SaleId = saleId;
    }
}
