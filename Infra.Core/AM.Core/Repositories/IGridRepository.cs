using AM.Core.Domain.Entities;
using AM.Core.Specification;

namespace AM.Core.Repositories
{
    public interface IGridRepository<TEntity> where TEntity : EntityBase, IAggregateRoot
    {
        ValueTask<long> CountAsync(IGridSpecification<TEntity> spec);
        Task<List<TEntity>> FindAsync(IGridSpecification<TEntity> spec);
    }
}
