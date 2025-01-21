namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidQuantityException : DomainException
{
    public int Quantity { get; }
    public int? MinQuantity { get; }

    public InvalidQuantityException(int quantity, string message)
        : base(message, "INVALID_QUANTITY")
    {
        Quantity = quantity;
    }

    public InvalidQuantityException(int quantity, int minQuantity)
        : base($"Quantity {quantity} is below the minimum of {minQuantity}", "INVALID_QUANTITY_MIN")
    {
        Quantity = quantity;
        MinQuantity = minQuantity;
    }

    public static InvalidQuantityException BelowMinimum(int quantity, int minQuantity)
        => new(quantity, minQuantity);

    public static InvalidQuantityException AboveMaximum(int quantity, int maxQuantity)
        => new(quantity, $"Quantity {quantity} exceeds maximum of {maxQuantity}");

}