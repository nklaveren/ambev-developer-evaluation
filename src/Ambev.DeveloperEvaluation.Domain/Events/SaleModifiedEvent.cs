namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModifiedEvent : DomainEvent
{
    public Guid Id { get; }
    public decimal OldTotalAmount { get; }
    public decimal NewTotalAmount { get; }
    public string ModificationType { get; }
    public string ModificationDetails { get; }

    public SaleModifiedEvent(
        Guid id,
        decimal oldTotalAmount,
        decimal newTotalAmount,
        string modificationType,
        string modificationDetails)
    {
        Id = id;
        OldTotalAmount = oldTotalAmount;
        NewTotalAmount = newTotalAmount;
        ModificationType = modificationType;
        ModificationDetails = modificationDetails;
    }
}