using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; } = "BRL";

    private Money() { }

    public Money(decimal amount, string currency = "BRL")
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Currency cannot be empty");

        if (amount < 0)
            throw new DomainException("Amount cannot be negative");

        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency.ToUpperInvariant();
    }

    public static Money FromDecimal(decimal amount, string currency = "BRL")
        => new(amount, currency);

    public static Money Zero(string currency = "BRL")
        => new(0, currency);

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
            throw new DomainException($"Cannot add money with different currencies. Expected {Currency} but got {other.Currency}");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
            throw new DomainException($"Cannot subtract money with different currencies. Expected {Currency} but got {other.Currency}");

        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(decimal multiplier)
        => new(Amount * multiplier, Currency);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public static Money operator +(Money left, Money right)
        => left.Add(right);

    public static Money operator -(Money left, Money right)
        => left.Subtract(right);

    public static Money operator *(Money left, decimal right)
        => left.Multiply(right);

    public static Money operator *(decimal left, Money right)
        => right.Multiply(left);
}