namespace AM.Core.Domain.CQRS.Queries
{
    public interface IListQuery<TResponse> : IQuery<TResponse> where TResponse : notnull
    {
        public List<string> Includes { get; init; }

    }
}
