using AM.Core.Domain.CQRS.Models;
using AM.Core.Domain.CQRS.Queries;
using AppContracts.Dtos;

namespace ProductService.Core.UseCases.Queries
{
    public class GetProductsQuery : IListQuery<ListResultModel<ProductDto>>
    {
        public List<string> Includes { get; init; } = new(new[] { "Returns", "Code" });
        public List<FilterModel> Filters { get; init; } = new();
        public List<string> Sorts { get; init; } = new();
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}