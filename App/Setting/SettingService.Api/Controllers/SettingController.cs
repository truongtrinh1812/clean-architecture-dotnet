using AM.Infra.Controller;
using AppContracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using SettingService.Core.UseCases.Queries;

namespace SettingService.Api.Controllers
{
    [ApiVersion("1.0")]
    public class SettingController : BaseController
    {
        [ApiVersion("1.0")]
        [HttpGet("/api/v{version:apiVersion}/countries/{id:guid}")]
        public async Task<ActionResult<CountryDto>> HandleAsync(Guid id,
            CancellationToken cancellationToken = new())
        {
            var request = new GetCountryByIdQuery { Id = id };

            return Ok(await Mediator.Send(request, cancellationToken));
        }
    }
}