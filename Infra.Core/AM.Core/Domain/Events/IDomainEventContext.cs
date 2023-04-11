namespace AM.Core.Domain.Events
{
    public interface IDomainEventContext
    {
        IEnumerable<EventBase> GetDomainEvents();
    }
}
