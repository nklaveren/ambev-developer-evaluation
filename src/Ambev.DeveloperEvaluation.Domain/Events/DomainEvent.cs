using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public abstract class DomainEvent : INotification
{
    public DateTime Timestamp { get; }
    public string EventType { get; }
    public string CorrelationId { get; }

    protected DomainEvent()
    {
        Timestamp = DateTime.UtcNow;
        EventType = GetType().Name;
        CorrelationId = Guid.NewGuid().ToString();
    }
}