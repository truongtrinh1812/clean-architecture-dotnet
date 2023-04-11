namespace AM.Core.Domain.Events
{
    public abstract class EventBase : IDomainEvent
    {
        public string EventType { get { return GetType().FullName; } }
        public DateTime CreateAt { get; } = DateTime.UtcNow;

        public IDictionary<string, object> MetaData { get; } = new Dictionary<string, object>();
        public string CorrelationId { get; init; }
        public abstract void Flatten ();

    }
}
