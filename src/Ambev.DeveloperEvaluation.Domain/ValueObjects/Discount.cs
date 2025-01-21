using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Discount : ValueObject
{
    public decimal Percentage { get; } = 0;
    public Money Amount { get; } = Money.Zero();
    public string Reason { get; } = string.Empty;

    private Discount() { }

    public Discount(decimal percentage, Money baseAmount, string reason = "")
    {
        if (percentage < 0 || percentage > 1)
            throw new DomainException("Discount percentage must be between 0 and 1");

        Percentage = percentage;
        Amount = baseAmount.Multiply(percentage);
        Reason = reason;
    }

    public static Discount None(string currency = "BRL")
        => new(0, Money.Zero(currency));

    public static Discount FromPercentage(decimal percentage, Money baseAmount, string reason = "")
        => new(percentage, baseAmount, reason);

    public Money ApplyTo(Money original)
    {
        if (original.Currency != Amount.Currency)
            throw new DomainException($"Cannot apply discount with different currency. Expected {Amount.Currency} but got {original.Currency}");

        return original.Subtract(Amount);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Percentage;
        yield return Amount.Amount;
        yield return Amount.Currency;
        yield return Reason;
    }
}