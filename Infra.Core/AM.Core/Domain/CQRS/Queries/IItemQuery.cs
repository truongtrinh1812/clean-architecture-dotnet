namespace AM.Core.Domain.CQRS.Queries
{
    public interface IItemQuery<TId, TResponse> : IQuery<TResponse>
        where TId : struct
        where TResponse : notnull
    {
        public List<string> Includes { get; init; }
        public TId Id { get; init; }
    }
}