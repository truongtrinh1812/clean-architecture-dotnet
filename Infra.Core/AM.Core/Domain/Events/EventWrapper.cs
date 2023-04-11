using MediatR;

namespace AM.Core.Domain.Events
{
    public class EventWrapper : INotification
    {
        public EventWrapper(IDomainEvent @event)
        {
            Event = @event;
        }

        public IDomainEvent Event { get; }
    }
}
