using AM.Core.Domain.CQRS.Models;
using AM.Core.Repositories;
using AppContracts.Dtos;
using MediatR;
using ProductService.Core.Core.Entities;
using ProductService.Core.Core.Specs;
using ProductService.Core.UseCases.Queries;

namespace ProductService.Core.UseCases.Handlers
{
    internal class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ResultModel<ProductDto>>
    {
        private readonly IRepository<Product> _productRepository;

        public GetProductByIdHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultModel<ProductDto>> Handle(GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new ProductByIdQuerySpec<ProductDto>(request);

            var product = await _productRepository.FindOneAsync(spec);

            return ResultModel<ProductDto>.Create(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Active = product.Active,
                Cost = product.Cost,
                Quantity = product.Quantity,
                Created = product.Created,
                Updated = product.Updated,
                ProductCodeId = product.ProductCodeId
            });
        }
    }
}