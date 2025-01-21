namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCanceledEvent : DomainEvent
{
    public Guid SaleId { get; }
    public DateTime CancellationDate { get; }
    public decimal CancelledAmount { get; }
    public string CancellationReason { get; }

    public SaleCanceledEvent(
        Guid saleId,
        decimal cancelledAmount,
        string cancellationReason = "")
    {
        SaleId = saleId;
        CancellationDate = DateTime.UtcNow;
        CancelledAmount = cancelledAmount;
        CancellationReason = cancellationReason;
    }
}