using Dapr.Client;
using AM.Infra.Bus;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace AM.Infra.TransactionalOutbox.Dapr.Internal
{
    public class TransactionalOutboxProcessor : ITransactionalOutboxProcessor
    {
        private readonly DaprClient _daprClient;
        private readonly IEventBus _eventBus;
        private readonly IOptions<DaprTransactionalOutboxOptions> _options;
        private readonly ILogger<TransactionalOutboxProcessor> _logger;

        public TransactionalOutboxProcessor(DaprClient daprClient, IEventBus eventBus, IOptions<DaprTransactionalOutboxOptions> options, ILogger<TransactionalOutboxProcessor> logger)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = default)
        {
            _logger.LogTrace("{TransactionalOutboxProcessor}: Cron @{DateTime}", nameof(TransactionalOutboxProcessor), DateTime.UtcNow);

            var events = await _daprClient.GetStateEntryAsync<List<OutboxEntity>>(_options.Value.StateStoreName, _options.Value.OutboxName, cancellationToken: cancellationToken);

            if (events?.Value is not { Count: > 0 }) return;

            var deletedEventIds = new List<Guid>();

            foreach (var domainEvent in events.Value)
            {
                if (domainEvent.Id.Equals(Guid.Empty) || string.IsNullOrEmpty(domainEvent.Type) || string.IsNullOrEmpty(domainEvent.Data)) continue;

                var @event = domainEvent.RecreateMessage(integrationAssemblyType.Assembly);

                await _eventBus.PublishAsync(@event, token: cancellationToken);

                deletedEventIds.Add(domainEvent.Id);
            }

            if (deletedEventIds.Count <= 0) return;

            foreach (var deletedEventId in deletedEventIds)
            {
                events.Value.RemoveAll(e => e.Id == deletedEventId);
            }

            await events.SaveAsync(cancellationToken: cancellationToken);
        }
    }
}