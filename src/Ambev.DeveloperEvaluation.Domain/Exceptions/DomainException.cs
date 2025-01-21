namespace Ambev.DeveloperEvaluation.Domain.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }
    public string Details { get; } = string.Empty;

    public DomainException(string message)
        : base(message)
    {
        Code = GetType().Name;
    }

    public DomainException(string message, string code)
        : base(message)
    {
        Code = code;
    }
}