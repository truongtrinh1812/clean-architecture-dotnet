using AM.Core.Domain.CQRS.Models;
using AM.Core.Repositories;
using AppContracts.Dtos;
using MediatR;
using ProductService.Core.Core.Entities;
using ProductService.Core.Core.Specs;
using ProductService.Core.UseCases.Queries;

namespace ProductService.Core.UseCases.Handlers
{
    internal class GetProductsHandler : IRequestHandler<GetProductsQuery, ResultModel<ListResultModel<ProductDto>>>
    {
        private readonly IGridRepository<Product> _productRepository;

        public GetProductsHandler(IGridRepository<Product> productRepository)
        {
            _productRepository =
                productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ResultModel<ListResultModel<ProductDto>>> Handle(GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var spec = new ProductListQuerySpec<ProductDto>(request);

            var products = await _productRepository.FindAsync(spec);

            var productModels = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active,
                Cost = x.Cost,
                Quantity = x.Quantity,
                Created = x.Created,
                Updated = x.Updated,
                ProductCodeId = x.ProductCodeId
            });

            var totalProducts = await _productRepository.CountAsync(spec);

            var resultModel = ListResultModel<ProductDto>.Create(
                productModels.ToList(), totalProducts, request.Page, request.PageSize);

            return ResultModel<ListResultModel<ProductDto>>.Create(resultModel);
        }
    }
}