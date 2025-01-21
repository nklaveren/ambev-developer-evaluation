namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidMoneyException : DomainException
{
    public decimal Amount { get; }
    public string Currency { get; }
    public string ExpectedCurrency { get; }

    private InvalidMoneyException(string message, string code, decimal amount = 0, string currency = "", string expectedCurrency = "")
        : base(message, code)
    {
        Amount = amount;
        Currency = currency;
        ExpectedCurrency = expectedCurrency;
    }

    public static InvalidMoneyException NegativeAmount(decimal amount)
        => new(
            $"Amount cannot be negative: {amount}",
            "NEGATIVE_AMOUNT",
            amount);

}