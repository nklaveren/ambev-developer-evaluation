using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Theory(DisplayName = "Sale should be created with valid items")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    public void Sale_ShouldBeCreatedWithValidItems_WithQuantity(int quantity)
    {
        var sale = SaleTestData.GetValidSaleWithItems(quantity);
        Assert.NotNull(sale);
        Assert.Equal(quantity, sale.Items.Count);
        Assert.Contains(sale.DomainEvents, e => e is SaleCreatedEvent);
        var saleCreatedEvent = sale.DomainEvents.OfType<SaleCreatedEvent>().First();
        Assert.Equal(sale.Id, saleCreatedEvent.Id);
        Assert.Equal(sale.CustomerId, saleCreatedEvent.CustomerId);
        Assert.Equal(sale.BranchId, saleCreatedEvent.BranchId);

        Assert.Equal(sale.TotalAmount.Amount, saleCreatedEvent.TotalAmount);
    }

    [Fact(DisplayName = "Sale should not be created with invalid customer")]
    public void Sale_ShouldNotBeCreatedWithInvalidCustomer()
    {
        Assert.Throws<DomainException>(() => SaleTestData.GetValidSaleWithItems(1, false));
    }

    [Fact(DisplayName = "Sale should not be created with invalid branch")]
    public void Sale_ShouldNotBeCreatedWithInvalidBranch()
    {
        Assert.Throws<DomainException>(() => SaleTestData.GetValidSaleWithItems(1, true, false));
    }

    [Fact(DisplayName = "Sale should be updated with valid items")]
    public void Sale_ShouldBeUpdatedWithValidItems()
    {
        var sale = SaleTestData.GetValidSaleWithItems(1);
        var items = SaleItemTestData.GetSaleItems(1, sale.Id);
        sale.Update(Guid.NewGuid(), Guid.NewGuid(), items);
        Assert.NotNull(sale);
        Assert.Equal(items.Count, sale.Items.Count);
        Assert.Contains(sale.DomainEvents, e => e is SaleModifiedEvent);
        var saleModifiedEvent = sale.DomainEvents.OfType<SaleModifiedEvent>().First();
        Assert.Equal(sale.Id, saleModifiedEvent.Id);
        Assert.Equal(sale.TotalAmount.Amount, saleModifiedEvent.OldTotalAmount);
        Assert.Equal(sale.TotalAmount.Amount, saleModifiedEvent.NewTotalAmount);
        Assert.Equal("SaleUpdated", saleModifiedEvent.ModificationType);
        Assert.Equal("Sale updated", saleModifiedEvent.ModificationDetails);
    }

    [Fact(DisplayName = "Sale should not be updated with invalid customer")]
    public void Sale_ShouldNotBeUpdatedWithInvalidCustomer()
    {
        var sale = SaleTestData.GetValidSaleWithItems(1);
        Assert.Throws<DomainException>(() => sale.Update(Guid.Empty, Guid.NewGuid(), SaleItemTestData.GetSaleItems(1, sale.Id)));
    }

    [Fact(DisplayName = "Sale should not be updated with invalid branch")]
    public void Sale_ShouldNotBeUpdatedWithInvalidBranch()
    {
        var sale = SaleTestData.GetValidSaleWithItems(1);
        Assert.Throws<DomainException>(() => sale.Update(Guid.NewGuid(), Guid.Empty, SaleItemTestData.GetSaleItems(1, sale.Id)));
    }

    [Fact(DisplayName = "Sale should be canceled")]
    public void Sale_ShouldBeCanceled()
    {
        var sale = SaleTestData.GetValidSaleWithItems(1);
        sale.Cancel();
        Assert.Equal(SaleStatus.Canceled, sale.Status);
        Assert.Contains(sale.DomainEvents, e => e is SaleCanceledEvent);
        Assert.All(sale.Items, item => Assert.True(item.IsCanceled));
        var saleCanceledEvent = sale.DomainEvents.OfType<SaleCanceledEvent>().First();
        Assert.Equal(sale.Id, saleCanceledEvent.SaleId);
        Assert.Equal(sale.TotalAmount.Amount, saleCanceledEvent.CancelledAmount);
        Assert.Equal("Sale canceled", saleCanceledEvent.CancellationReason);

        foreach (var item in sale.Items)
        {
            var saleItemCanceledEvent = item.DomainEvents.OfType<SaleItemCanceledEvent>().First(e => e.ProductId == item.ProductId);
            Assert.Equal(sale.Id, saleItemCanceledEvent.SaleId);
            Assert.Equal(item.Quantity, saleItemCanceledEvent.Quantity);
            Assert.Equal(item.UnitPrice.Amount, saleItemCanceledEvent.UnitPrice);
            Assert.Equal(item.TotalAmount.Amount, saleItemCanceledEvent.TotalAmount);
        }
    }

    [Fact(DisplayName = "Sale should not be canceled if it is already canceled")]
    public void Sale_ShouldNotBeCanceledIfItIsAlreadyCanceled()
    {
        var sale = SaleTestData.GetValidSaleWithItems(1);
        sale.Cancel();
        Assert.Throws<InvalidSaleStateException>(() => sale.Cancel());
    }

}