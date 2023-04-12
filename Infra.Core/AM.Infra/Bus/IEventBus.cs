using AM.Core.Domain.Events;

namespace AM.Infra.Bus
{
    public interface IEventBus
    {
        Task PublishAsync<TEvent>(TEvent @event, string[] topics = default, CancellationToken token = default) where TEvent : IDomainEvent;
        Task SubscribeAsync<TEvent>(string[] topics = default, CancellationToken token = default) where TEvent : IDomainEvent;
    }
}