using AM.Core.Domain.Events;

namespace IntegrationEvents.Customer
{
    [DaprPubSubName(PubSubName = "pubsub")]
    public class CustomerCreatedIntegrationEvent : EventBase
    {
        public override void Flatten()
        {
        }
    }
}