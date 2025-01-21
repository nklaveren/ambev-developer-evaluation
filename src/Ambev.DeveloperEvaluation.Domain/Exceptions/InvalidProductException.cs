
namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class InvalidProductException : DomainException
{
    public InvalidProductException(string message, string code) : base(message, code) { }

    internal static InvalidProductException InvalidId(Guid productId, string message)
    {
        return new InvalidProductException($"Product {productId} is invalid. {message}", "INVALID_PRODUCT");
    }
}