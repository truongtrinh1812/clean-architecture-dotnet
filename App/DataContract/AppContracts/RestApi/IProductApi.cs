using AppContracts.Common;
using AppContracts.Dtos;

using RestEase;

namespace AppContracts.RestApi
{
    public interface IProductApi
    {
        [Get("product-api/v1/products")]
        Task<ResultDto<ListResultDto<ProductDto>>> GetProductsAsync(
            [Header("x-query")] string xQuery
        );
    }
}
