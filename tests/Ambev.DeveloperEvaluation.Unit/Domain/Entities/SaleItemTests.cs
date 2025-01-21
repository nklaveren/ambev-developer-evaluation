using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact(DisplayName = "SaleItem should be created with valid values")]
    public void SaleItem_ShouldBeCreatedWithValidValues()
    {
        var saleItem = SaleItemTestData.GetItemWithZeroDiscount();
        Assert.NotNull(saleItem);
        Assert.NotEqual(Guid.Empty, saleItem.Id);
        Assert.Equal(Guid.Empty, saleItem.SaleId);
        Assert.NotEqual(Guid.Empty, saleItem.ProductId);
        Assert.True(saleItem.Quantity > 0);
        Assert.Equal(Discount.None(), saleItem.Discount);
    }

    [Fact(DisplayName = "SaleItem should be created with valid values")]
    public void SaleItem_ShouldBeCreatedWithValidValues_WithDiscount()
    {
        var saleItem = SaleItemTestData.GetItemWithZeroDiscount();
        Assert.NotNull(saleItem);
        Assert.NotEqual(Guid.Empty, saleItem.Id);
        Assert.Equal(Guid.Empty, saleItem.SaleId);
        Assert.NotEqual(Guid.Empty, saleItem.ProductId);
        Assert.True(saleItem.Quantity > 0);
        Assert.Equal(Discount.None(), saleItem.Discount);
    }

    [Fact(DisplayName = "SaleItem should not be created with invalid quantity")]
    public void SaleItem_ShouldNotBeCreatedWithInvalidQuantity()
    {
        var exception = Assert.Throws<InvalidQuantityException>(() => SaleItemTestData.GetItemWithQuantity(0));
        Assert.Equal("INVALID_QUANTITY_MIN", exception.Code);
        Assert.Equal(0, exception.Quantity);
    }

    [Fact(DisplayName = "SaleItem should not be created with invalid quantity")]
    public void SaleItem_ShouldNotBeCreatedWithInvalidQuantity_BelowMinimum()
    {
        var exception = Assert.Throws<InvalidQuantityException>(() => SaleItemTestData.GetItemWithQuantity(0));
        Assert.Equal("INVALID_QUANTITY_MIN", exception.Code);
        Assert.Equal(0, exception.Quantity);
        Assert.Equal(1, exception.MinQuantity);
    }

    [Fact(DisplayName = "SaleItem should not be created with invalid quantity")]
    public void SaleItem_ShouldNotBeCreatedWithInvalidQuantity_AboveMaximum()
    {
        var exception = Assert.Throws<InvalidQuantityException>(() => SaleItemTestData.GetItemWithQuantity(21));
        Assert.Equal("INVALID_QUANTITY", exception.Code);
        Assert.Equal(21, exception.Quantity);
    }

    [Fact(DisplayName = "SaleItem should not be created with invalid product")]
    public void SaleItem_ShouldNotBeCreatedWithInvalidProduct()
    {
        var exception = Assert.Throws<InvalidProductException>(() => SaleItemTestData.GetItemWithInvalidProduct(Guid.Empty));
        Assert.Equal("INVALID_PRODUCT", exception.Code);
    }

    [Fact(DisplayName = "SaleItem should not be created with invalid unit price")]
    public void SaleItem_ShouldNotBeCreatedWithInvalidUnitPrice()
    {
        var exception = Assert.Throws<InvalidMoneyException>(() => SaleItemTestData.GetItemWithUnitPrice(-10));
        Assert.Equal("NEGATIVE_AMOUNT", exception.Code);
        Assert.Equal(-10, exception.Amount);
        Assert.Equal("", exception.ExpectedCurrency);
        Assert.Equal("", exception.Currency);
    }

    [Fact(DisplayName = "SaleItem should be canceled")]
    public void SaleItem_ShouldBeCanceled()
    {
        var saleItem = SaleItemTestData.GetValidItem(Guid.Empty);
        saleItem.Cancel();
        Assert.True(saleItem.IsCanceled);
    }

    [Fact(DisplayName = "SaleItem should not be canceled if it is already canceled")]
    public void SaleItem_ShouldNotBeCanceledIfItIsAlreadyCanceled()
    {
        var saleItem = SaleItemTestData.GetValidItem(Guid.Empty);
        saleItem.Cancel();
        var exception = Assert.Throws<InvalidSaleStateException>(() => saleItem.Cancel());
        Assert.Equal("SALE_ALREADY_CANCELLED", exception.Code);
        Assert.Contains("Sale", exception.Message);
        Assert.Equal("Cancelled", exception.CurrentState);
        Assert.Equal("Cancel", exception.RequestedOperation);
    }
}