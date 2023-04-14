using AM.Core.Domain.CQRS.Commands;
using AppContracts.Dtos;

namespace ProductService.Core.UseCases.Commands
{
    public record CreateProductCommand : ICreateCommand<CreateProductCommand.CreateProductModel, ProductDto>
    {
        public CreateProductModel Model { get; init; } = default!;
        public record CreateProductModel(string Name, int Quantity, decimal Cost, string ProductCodeName);
    }
}