using AM.Core.Domain.Events;
using Dapr.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace AM.Infra.Bus.Dapr
{
    internal class DaprEventBus : IEventBus
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<DaprEventBus> _logger;
        private readonly IOptions<DaprEventBusOptions> _options;

        public DaprEventBus(DaprClient daprClient,
                            IOptions<DaprEventBusOptions> options,
                            ILogger<DaprEventBus> logger)
        {
            _daprClient = daprClient ?? throw new ArgumentNullException(nameof(daprClient));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        async Task IEventBus.PublishAsync<TEvent>(TEvent @event, string[] topics, CancellationToken token)
        {
            var attr = (DaprPubSubNameAttribute)Attribute.GetCustomAttribute(typeof(TEvent), typeof(DaprPubSubNameAttribute));

            var pubsubName = _options.Value.PubSubName ?? "pubsub";

            if (attr is not null)
            {
                pubsubName = attr.PubSubName;
            }

            if (topics is null)
            {
                var topicName = @event.GetType().Name;

                _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, pubsubName, topicName);
                await _daprClient.PublishEventAsync(pubsubName, topicName, @event, token);
            }
            else
            {
                foreach (var topicName in topics)
                {
                    _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, pubsubName,
                        topicName);
                    await _daprClient.PublishEventAsync(pubsubName, topicName, @event, token);
                }
            }
        }

        Task IEventBus.SubscribeAsync<TEvent>(string[] topics, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}