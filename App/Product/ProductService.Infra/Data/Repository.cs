using AM.Core.Domain.Entities;
using AM.Infra.EFCore.Persistence;

namespace ProductService.Infra.Data
{
    public class Repository<TEntity> : RepositoryBase<MainDbContext, TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        public Repository(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}