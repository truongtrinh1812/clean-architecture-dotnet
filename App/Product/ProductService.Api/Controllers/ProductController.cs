using AM.Core.Domain.CQRS.Models;
using AM.Infra;
using AM.Infra.Controller;
using AppContracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Core.UseCases.Commands;
using ProductService.Core.UseCases.Queries;

namespace ProductService.Api.Controllers
{
    [Authorize("RequireInteractiveUser")]
    [ApiVersion("1.0")]
    public class ProductController : BaseController
    {
        [HttpGet("/api/v{version:apiVersion}/products/{id:guid}")]
        public async Task<ActionResult<ProductDto>> HandleGetProductByIdAsync(Guid id,
                    CancellationToken cancellationToken = new())
        {
            var request = new GetProductByIdQuery { Id = id };

            return Ok(await Mediator.Send(request, cancellationToken));
        }

        [HttpGet("/api/v{version:apiVersion}/products")]
        public async Task<ActionResult> HandleGetProductsAsync([FromHeader(Name = "x-query")] string query,
            CancellationToken cancellationToken = new())
        {
            var queryModel = HttpContext.SafeGetListQuery<GetProductsQuery, ListResultModel<ProductDto>>(query);

            return Ok(await Mediator.Send(queryModel, cancellationToken));
        }

        [HttpPost("/api/v{version:apiVersion}/products")]
        public async Task<ActionResult> HandleCreateProductAsync([FromBody] CreateProductCommand request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}