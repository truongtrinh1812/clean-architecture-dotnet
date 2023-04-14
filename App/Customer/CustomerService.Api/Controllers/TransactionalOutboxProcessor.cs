using AM.Infra.Controller;
using AM.Infra.TransactionalOutbox.Dapr;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TransactionalOutboxProcessor : BaseController
    {
        private readonly ITransactionalOutboxProcessor _outboxProcessor;

        public TransactionalOutboxProcessor(ITransactionalOutboxProcessor outboxProcessor)
        {
            _outboxProcessor = outboxProcessor ?? throw new ArgumentNullException(nameof(outboxProcessor));
        }

        [HttpPost("customer-outbox-cron")]
        public async Task<ActionResult> HandleProductOutboxCronAsync(CancellationToken cancellationToken = new())
        {
            await _outboxProcessor.HandleAsync(typeof(IntegrationEvents.Anchor), cancellationToken);

            return Ok();
        }
    }
}