namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent : DomainEvent
{
    public Guid Id { get; }
    public DateTime SaleDate { get; }
    public Guid CustomerId { get; }
    public Guid BranchId { get; }
    public decimal TotalAmount { get; }

    public SaleCreatedEvent(Guid id, DateTime saleDate, Guid customerId, Guid branchId, decimal totalAmount)
    {
        Id = id;
        SaleDate = saleDate;
        CustomerId = customerId;
        BranchId = branchId;
        TotalAmount = totalAmount;
    }
}