using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using AM.Core.Domain.CQRS.Queries;
using AM.Core.Specification;
using ProductService.Core.Core.Entities;

namespace ProductService.Core.Core.Specs
{
    public class ProductByIdQuerySpec<TResponse> : SpecificationBase<Product>
    {
        private readonly Guid _id;

        public ProductByIdQuerySpec([NotNull] IItemQuery<Guid, TResponse> queryInput)
        {
            ApplyIncludeList(queryInput.Includes);

            _id = queryInput.Id;
        }

        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}