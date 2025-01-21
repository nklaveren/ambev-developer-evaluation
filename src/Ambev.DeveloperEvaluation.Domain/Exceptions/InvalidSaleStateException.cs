namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidSaleStateException : DomainException
{
    public Guid SaleId { get; }
    public string CurrentState { get; }
    public string RequestedOperation { get; }

    private InvalidSaleStateException(string message, string code, Guid saleId, string currentState = "", string requestedOperation = "")
        : base(message, code)
    {
        SaleId = saleId;
        CurrentState = currentState;
        RequestedOperation = requestedOperation;
    }

    public static InvalidSaleStateException AlreadyCancelled(Guid saleId)
        => new(
            $"Sale {saleId} is already cancelled",
            "SALE_ALREADY_CANCELLED",
            saleId,
            "Cancelled",
            "Cancel");

}