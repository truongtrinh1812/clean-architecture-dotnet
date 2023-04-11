using AM.Core.Domain.CQRS.Models;
using MediatR;

namespace AM.Core.Domain.CQRS.Commands
{
    public interface ICommand<T> : IRequest<ResultModel<T>>
         where T : notnull
    {
    }
}
