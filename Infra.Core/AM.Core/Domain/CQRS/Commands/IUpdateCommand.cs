using AM.Core.Domain.Entities;

namespace AM.Core.Domain.CQRS.Commands
{
    public interface IUpdateCommand<TRequest, TResponse> : ICommand<TResponse>, ITxRequest
        where TRequest : notnull
        where TResponse : notnull
    {
        public TRequest Model { get; init; }
    }
}
