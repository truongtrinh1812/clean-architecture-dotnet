using AM.Infra.Controller;
using CustomerService.Core.UseCases.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpPost("/api/v{version:apiVersion}/customers")]
        public async Task<ActionResult> HandleAsync([FromBody] CreateCustomerCommand request, CancellationToken cancellationToken = new())
        {
            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}