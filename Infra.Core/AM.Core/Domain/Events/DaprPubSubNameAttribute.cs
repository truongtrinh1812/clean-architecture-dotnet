namespace AM.Core.Domain.Events
{
    public class DaprPubSubNameAttribute : Attribute
    {
        public string PubSubName { get; set; } = "pubsub";
    }
}
