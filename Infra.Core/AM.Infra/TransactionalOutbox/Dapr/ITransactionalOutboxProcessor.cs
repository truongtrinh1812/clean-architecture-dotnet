namespace AM.Infra.TransactionalOutbox.Dapr
{
    public interface ITransactionalOutboxProcessor
    {
        Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new());
    }
}