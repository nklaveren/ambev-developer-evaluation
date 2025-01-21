namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleItemCanceledEvent : DomainEvent
{
    public Guid Id { get; }
    public Guid SaleId { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal TotalAmount { get; }
    public DateTime CancellationDate { get; }

    public SaleItemCanceledEvent(
        Guid id,
        Guid saleId,
        Guid productId,
        int quantity,
        decimal unitPrice,
        decimal totalAmount)
    {
        Id = id;
        SaleId = saleId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalAmount = totalAmount;
        CancellationDate = DateTime.UtcNow;
    }
}