using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects;

public class DiscountTests
{
    [Fact(DisplayName = "Discount should be created with zero values")]
    public void Discount_ShouldBeCreatedWithZeroValues()
    {
        var discount = Discount.None();
        Assert.NotNull(discount);
        Assert.Equal(0, discount.Percentage);
        Assert.Equal(Money.Zero(), discount.Amount);
    }

    [Fact(DisplayName = "Discount should be created with percentage and amount")]
    public void Discount_ShouldBeCreatedWithPercentageAndAmount()
    {
        var discount = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"));
        Assert.NotNull(discount);
        Assert.Equal(0.1M, discount.Percentage);
        Assert.Equal(Money.FromDecimal(10, "USD"), discount.Amount);
    }

    [Fact(DisplayName = "Discount cannot apply discount with different currency")]
    public void Discount_CannotApplyDiscountWithDifferentCurrency()
    {
        var discount = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"));
        var money = Money.FromDecimal(100, "BRL");
        Assert.Throws<DomainException>(() => discount.ApplyTo(money));
    }

    [Fact(DisplayName = "Discount should apply discount to money")]
    public void Discount_ShouldApplyDiscountToMoney()
    {
        var discount = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"));
        var money = Money.FromDecimal(100, "USD");
        var result = discount.ApplyTo(money);
        Assert.Equal(Money.FromDecimal(90, "USD"), result);
    }

    [Fact(DisplayName = "Discount percentage must be between 0 and 1")]
    public void Discount_PercentageMustBeBetween0And1()
    {
        Assert.Throws<DomainException>(() => Discount.FromPercentage(-0.1M, Money.FromDecimal(100, "USD")));
        Assert.Throws<DomainException>(() => Discount.FromPercentage(1.1M, Money.FromDecimal(100, "USD")));
    }

    [Fact(DisplayName = "Discount amount cannot be negative")]
    public void Discount_AmountCannotBeNegative()
    {
        Assert.Throws<DomainException>(() => Discount.FromPercentage(0.1M, Money.FromDecimal(-100, "USD")));
    }

    [Fact(DisplayName = "Discount reason cannot be empty")]
    public void Discount_ReasonCannotBeEmpty()
    {
        var discount = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"));
        Assert.NotNull(discount);
        Assert.Equal(string.Empty, discount.Reason);
    }

    [Fact(DisplayName = "Discoun should be equals when percentage, amount and reason are the same")]
    public void Discount_ShouldBeEqualsWhenPercentageAmountAndReasonAreTheSame()
    {
        var discount1 = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"), "Reason 1");
        var discount2 = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"), "Reason 1");
        Assert.Equal(discount1, discount2);
    }

    [Fact(DisplayName = "Discount should not be equals when percentage, amount or reason are different")]
    public void Discount_ShouldNotBeEqualsWhenPercentageAmountOrReasonAreDifferent()
    {
        var discount1 = Discount.FromPercentage(0.1M, Money.FromDecimal(100, "USD"), "Reason 1");
        var discount2 = Discount.FromPercentage(0.2M, Money.FromDecimal(100, "USD"), "Reason 1");
        Assert.NotEqual(discount1, discount2);
    }
}
