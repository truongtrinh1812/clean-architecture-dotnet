using AM.Core.Domain.CQRS.Models;
using AM.Core.Repositories;
using AppContracts.Dtos;
using MediatR;
using ProductService.Core.Core.Entities;
using ProductService.Core.UseCases.Commands;

namespace ProductService.Core.UseCases.Handlers
{
    internal class CreateProductHandler : IRequestHandler<CreateProductCommand, ResultModel<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCode> _productCodeRepository;

        public CreateProductHandler(IRepository<Product> productRepository,
            IRepository<ProductCode> productCodeRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _productCodeRepository = productCodeRepository ??
                                     throw new ArgumentNullException(nameof(productCodeRepository));
        }

        public async Task<ResultModel<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productCode =
                await _productCodeRepository.AddAsync(ProductCode.Create(request.Model.ProductCodeName));
            if (productCode is null)
            {
                throw new Exception($"Couldn't find Product Code with name={request.Model.ProductCodeName}");
            }

            var created = await _productRepository.AddAsync(
                Product.Create(
                    request.Model.Name,
                    request.Model.Quantity,
                    request.Model.Cost,
                    productCode));

            return ResultModel<ProductDto>.Create(new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Active = created.Active,
                Cost = created.Cost,
                Quantity = created.Quantity,
                Created = created.Created,
                Updated = created.Updated,
                ProductCodeId = created.ProductCodeId
            });
        }
    }
}