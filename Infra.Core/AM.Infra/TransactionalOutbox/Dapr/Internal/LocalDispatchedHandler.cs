using AM.Core.Domain.Events;
using MediatR;
using Microsoft.Extensions.Options;
using Dapr.Client;

namespace AM.Infra.TransactionalOutbox.Dapr.Internal
{
    internal class LocalDispatchedHandler : INotificationHandler<EventWrapper>
    {
        private readonly DaprClient _daprClient;
        private readonly IOptions<DaprTransactionalOutboxOptions> _options;

        public LocalDispatchedHandler(DaprClient daprClient, IOptions<DaprTransactionalOutboxOptions> options)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Handle(EventWrapper @eventWrapper, CancellationToken cancellationToken)
        {
            var events = await _daprClient.GetStateEntryAsync<List<OutboxEntity>>(_options.Value.StateStoreName, _options.Value.OutboxName, cancellationToken: cancellationToken);
            events.Value ??= new List<OutboxEntity>();

            var outboxEntity = new OutboxEntity(Guid.NewGuid(), DateTime.UtcNow, @eventWrapper.Event);

            events.Value.Add(outboxEntity);

            await events.SaveAsync(cancellationToken: cancellationToken);
        }
    }
}