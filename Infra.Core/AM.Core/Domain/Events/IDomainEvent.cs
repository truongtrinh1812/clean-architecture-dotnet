using MediatR;

namespace AM.Core.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime CreateAt { get; }
        IDictionary<string, object> MetaData { get; }
    }
}
