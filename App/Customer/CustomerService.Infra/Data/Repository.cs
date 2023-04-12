using AM.Core.Domain.Entities;
using AM.Infra.EFCore.Persistence;

namespace CustomerService.Infra.Data
{
    public class Repository<TEntity> : RepositoryBase<MainDbContext, TEntity> where TEntity : EntityRootBase
    {
        public Repository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}