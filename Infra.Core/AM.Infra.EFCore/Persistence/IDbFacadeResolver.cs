using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AM.Infra.EFCore.Persistence
{
    public interface IDbFacadeResolver
    {
        DatabaseFacade Database { get; }
    }
}
