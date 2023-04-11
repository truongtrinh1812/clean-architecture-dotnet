namespace AM.Core.Domain.CQRS.Commands
{
    public interface IDeleteCommand<TId, TResponse> : ICommand<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        public TId Id { get; init; }
    }
}
