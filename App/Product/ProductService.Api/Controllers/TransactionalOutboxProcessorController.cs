using AM.Infra.Controller;
using AM.Infra.TransactionalOutbox.Dapr;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Api.Controllers
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TransactionalOutboxProcessorController : BaseController
    {
        private readonly ITransactionalOutboxProcessor _outboxProcessor;

        public TransactionalOutboxProcessorController(ITransactionalOutboxProcessor outboxProcessor)
        {
            _outboxProcessor = outboxProcessor ?? throw new ArgumentNullException(nameof(outboxProcessor));
        }

        [HttpPost("product-outbox-cron")]
        public async Task<ActionResult> HandleProductOutboxCronAsync(CancellationToken cancellationToken = new())
        {
            await _outboxProcessor.HandleAsync(typeof(IntegrationEvents.Anchor), cancellationToken);

            return Ok();
        }
    }
}