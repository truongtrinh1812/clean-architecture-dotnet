using AM.Core.Domain.CQRS.Models;
using AM.Core.Domain.CQRS.Queries;
using AM.Core.Specification;
using ProductService.Core.Core.Entities;

namespace ProductService.Core.Core.Specs
{
    public class ProductListQuerySpec<TResponse> : GridSpecificationBase<Product>
    {
        public ProductListQuerySpec(IListQuery<ListResultModel<TResponse>> gridQueryInput)
        {
            ApplyIncludeList(gridQueryInput.Includes);

            ApplyFilterList(gridQueryInput.Filters);

            ApplySortingList(gridQueryInput.Sorts);

            ApplyPaging(gridQueryInput.Page, gridQueryInput.PageSize);
        }
    }
}