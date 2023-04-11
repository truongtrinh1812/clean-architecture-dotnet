using AM.Core.Domain.Events;

namespace AM.Core.Domain.Entities
{
    public abstract class EntityRootBase : EntityBase, IAggregateRoot
    {
        public HashSet<EventBase> DomainEvents { get; private set; }
        public void AddDomainEvent(EventBase eventItem)
        {
            DomainEvents ??= new HashSet<EventBase>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(EventBase eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }
    }
}
