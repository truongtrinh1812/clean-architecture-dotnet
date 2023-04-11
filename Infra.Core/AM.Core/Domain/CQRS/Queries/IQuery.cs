using AM.Core.Domain.CQRS.Models;
using MediatR;

namespace AM.Core.Domain.CQRS.Queries
{
    public interface IQuery<T> : IRequest<ResultModel<T>> where T : notnull
    {
    }
}
