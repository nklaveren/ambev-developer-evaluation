using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    protected Sale() { }

    public DateTime SaleDate { get; private set; }
    public Guid CustomerId { get; private set; }
    public Money TotalAmount { get; private set; } = Money.Zero();
    public Guid BranchId { get; private set; }
    public SaleStatus Status { get; private set; } = SaleStatus.NotCanceled;
    private readonly List<SaleItem> _items = [];
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    public Sale(Guid id, Guid customerId, Guid branchId, List<SaleItem> items)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("CustomerId is required");

        if (branchId == Guid.Empty)
            throw new DomainException("BranchId is required");

        CustomerId = customerId;
        BranchId = branchId;
        SaleDate = DateTime.UtcNow;
        Status = SaleStatus.NotCanceled;
        foreach (var item in items)
        {
            item.UpdateSaleId(id);
            _items.Add(item);
        }
        RecalculateTotalAmount();

        AddDomainEvent(new SaleCreatedEvent(Id, SaleDate, CustomerId, BranchId, TotalAmount.Amount));
    }

    public void Update(Guid customerId, Guid branchId, List<SaleItem> items)
    {
        if (customerId == Guid.Empty)
            throw new DomainException("CustomerId is required");

        if (branchId == Guid.Empty)
            throw new DomainException("BranchId is required");

        CustomerId = customerId;
        BranchId = branchId;
        _items.Clear();
        _items.AddRange(items);

        RecalculateTotalAmount();
        AddDomainEvent(new SaleModifiedEvent(
            Id,
            TotalAmount.Amount,
            TotalAmount.Amount,
            "SaleUpdated",
            "Sale updated"));
    }

    public void Cancel()
    {
        if (Status == SaleStatus.Canceled)
            throw InvalidSaleStateException.AlreadyCancelled(Id);

        Status = SaleStatus.Canceled;
        foreach (var item in _items)
        {
            if (!item.IsCanceled)
                item.Cancel();
        }
        AddDomainEvent(new SaleCanceledEvent(Id, TotalAmount.Amount, "Sale canceled"));
    }

    private void RecalculateTotalAmount()
    {
        TotalAmount = _items.Aggregate(
            Money.Zero(),
            (current, item) => current.Add(item.TotalAmount));
    }
}