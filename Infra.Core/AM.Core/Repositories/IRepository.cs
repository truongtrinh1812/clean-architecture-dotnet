using AM.Core.Domain.Entities;
using AM.Core.Specification;

namespace AM.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity: EntityBase, IAggregateRoot
    {
        TEntity FindById(Guid id);
        Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);
        Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
