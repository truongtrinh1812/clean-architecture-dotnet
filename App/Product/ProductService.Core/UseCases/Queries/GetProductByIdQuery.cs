using AM.Core.Domain.CQRS.Queries;
using AppContracts.Dtos;

namespace ProductService.Core.UseCases.Queries
{
    public record GetProductByIdQuery : IItemQuery<Guid, ProductDto>
    {
        public List<string> Includes { get; init; } = new(new[] { "Returns", "Code" });
        public Guid Id { get; init; }
    }
}