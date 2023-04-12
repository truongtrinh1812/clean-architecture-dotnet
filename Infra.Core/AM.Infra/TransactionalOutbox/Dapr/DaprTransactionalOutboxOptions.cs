namespace AM.Infra.TransactionalOutbox.Dapr
{
    public class DaprTransactionalOutboxOptions
    {
        public static string Name = "DaprTransactionalOutbox";
        public string StateStoreName { get; set; } = "statestore";
        public string OutboxName { get; set; } = "outbox";
    }
}