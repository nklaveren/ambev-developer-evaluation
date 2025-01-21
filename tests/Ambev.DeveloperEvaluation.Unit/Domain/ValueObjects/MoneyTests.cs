using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects;

public class MoneyTests
{
    [Fact(DisplayName = "Money should be created with valid values")]
    public void Money_ShouldBeCreatedWithValidValues()
    {
        var money = Money.FromDecimal(100);
        Assert.NotNull(money);
        Assert.Equal(100, money.Amount);
        Assert.Equal("BRL", money.Currency);
    }

    [Fact(DisplayName = "Money should be created with zero values")]
    public void Money_ShouldBeCreatedWithZeroValues()
    {
        var money = Money.Zero();
        Assert.NotNull(money);
        Assert.Equal(0, money.Amount);
        Assert.Equal("BRL", money.Currency);
    }

    [Fact(DisplayName = "Money should not be created with invalid values")]
    public void Money_ShouldNotBeCreatedWithInvalidValues()
    {
        Assert.Throws<DomainException>(() => Money.FromDecimal(-100));
    }

    [Fact(DisplayName = "Money should not be created with invalid currency")]
    public void Money_ShouldNotBeCreatedWithInvalidCurrency()
    {
        Assert.Throws<DomainException>(() => Money.FromDecimal(100, ""));
    }

    [Fact(DisplayName = "Money should be created with valid currency")]
    public void Money_ShouldBeCreatedWithValidCurrency()
    {
        var money = Money.FromDecimal(100, "USD");
        Assert.NotNull(money);
        Assert.Equal(100, money.Amount);
        Assert.Equal("USD", money.Currency);
    }

    [Fact(DisplayName = "Money with different currencies should not be added")]
    public void Money_WithDifferentCurrenciesShouldNotBeAdded()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "BRL");
        Assert.Throws<DomainException>(() => money1 + money2);
    }

    [Fact(DisplayName = "Cannot subtract money with different currencies")]
    public void Money_WithDifferentCurrenciesShouldNotBeSubtracted()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "BRL");
        Assert.Throws<DomainException>(() => money1 - money2);
    }

    [Fact(DisplayName = "Money with the same currency should be added")]
    public void Money_WithSameCurrenciesShouldBeAdded()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "USD");
        var result = money1 + money2;
        Assert.Equal(200, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact(DisplayName = "Money with the same currency should be subtracted")]
    public void Money_WithSameCurrenciesShouldBeSubtracted()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "USD");
        var result = money1 - money2;
        Assert.Equal(0, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact(DisplayName = "Money should be multiplied by a decimal")]
    public void Money_ShouldBeMultipliedByADecimal()
    {
        var money = Money.FromDecimal(100, "USD");
        var result = money * 2M;
        Assert.Equal(200, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact(DisplayName = "Money Equals should return true if the amount and currency are the same    ")]
    public void Money_EqualsShouldReturnTrueIfTheAmountAndCurrencyAreTheSame()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "USD");
        Assert.True(money1.Equals(money2));
    }

    [Fact(DisplayName = "Money Equals should return false if the amount or currency are different")]
    public void Money_EqualsShouldReturnFalseIfTheAmountOrCurrencyAreDifferent()
    {
        var money1 = Money.FromDecimal(100, "USD");
        var money2 = Money.FromDecimal(100, "BRL");
        Assert.False(money1.Equals(money2));
    }

    [Fact(DisplayName = "Money Multiply should return the correct amount")]
    public void Money_MultiplyShouldReturnTheCorrectAmount()
    {
        var money = Money.FromDecimal(100, "USD");
        var result = 2M * money;
        Assert.Equal(200, result.Amount);
        Assert.Equal("USD", result.Currency);
    }

    [Fact(DisplayName = "Money currency cannot be empty")]
    public void Money_CurrencyCannotBeEmpty()
    {
        Assert.Throws<DomainException>(() => Money.FromDecimal(100, ""));
    }

    [Fact(DisplayName = "Money amount cannot be negative")]
    public void Money_AmountCannotBeNegative()
    {
        Assert.Throws<DomainException>(() => Money.FromDecimal(-100, "USD"));
    }
}