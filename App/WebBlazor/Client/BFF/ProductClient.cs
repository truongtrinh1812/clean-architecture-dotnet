
using AppContracts.Common;
using System.Net.Http.Json;
using AppContracts.Dtos;


public class ProductClient
{
    private readonly HttpClient http;
    private ResultDto<ListResultDto<ProductDto>>? products;


    public ProductClient(HttpClient http)
    {
        this.http = http;
    }

    public async Task<ResultDto<ListResultDto<ProductDto>>> GetProductsAsync()
    {
        products = await http.GetFromJsonAsync<ResultDto<ListResultDto<ProductDto>>>(
            "gateway/product-api/v1/products");

        return products;
    }
}
