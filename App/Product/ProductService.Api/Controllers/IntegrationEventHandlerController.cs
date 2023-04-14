using AM.Infra.Controller;
using Dapr;
using IntegrationEvents.Customer;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Api.Controllers
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v1/[controller]")]
    public class IntegrationEventHandlerController : BaseController
    {
        private readonly ILogger<IntegrationEventHandlerController> _logger;

        public IntegrationEventHandlerController(ILogger<IntegrationEventHandlerController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("customer-created")]
        [Topic("pubsub", "CustomerCreatedIntegrationEvent")]
#pragma warning disable 1998
        public async Task<ActionResult> HandleCustomerCreatedAsync(CustomerCreatedIntegrationEvent @event,
#pragma warning restore 1998
            CancellationToken cancellationToken = new())
        {
            _logger.LogInformation($"I received the message with name={@event.GetType().FullName}");

            // TODO: this is an example for pub/sub
            return Ok();
        }
    }
}