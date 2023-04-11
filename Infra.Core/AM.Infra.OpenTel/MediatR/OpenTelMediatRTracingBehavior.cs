using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTelemetry.Trace;

namespace AM.Infra.OpenTel.MediatR
{
    public class OpenTelMediatRTracingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly ActivitySource ActivitySource = new(OpenTelMediatROptions.OpenTelMediatRName);
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<OpenTelMediatRTracingBehavior<TRequest, TResponse>> _logger;

        public OpenTelMediatRTracingBehavior(IHttpContextAccessor httpContextAccessor, ILogger<OpenTelMediatRTracingBehavior<TRequest, TResponse>> logger)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? _httpContextAccessor?.HttpContext?.TraceIdentifier;
            const string prefix = nameof(OpenTelMediatRTracingBehavior<TRequest, TResponse>);
            var handlerName = typeof(TRequest).Name.Replace("Query", "Handler"); // by convention

            _logger.LogInformation(
                "[{Prefix}:{HandlerName}] Handle {X-RequestData} request with TraceId={TraceId}",
                prefix, handlerName, typeof(TRequest).Name, traceId);

            using var activity = ActivitySource.StartActivity($"{OpenTelMediatROptions.OpenTelMediatRName}.{handlerName}", ActivityKind.Server);

            activity?.AddEvent(new ActivityEvent(handlerName))
                ?.AddTag("params.request.name", typeof(TRequest).Name)
                ?.AddTag("params.response.name", typeof(TResponse).Name);

            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                activity.SetStatus(Status.Error.WithDescription(ex.Message));
                activity.RecordException(ex);

                _logger.LogError(ex, "[{Prefix}:{HandlerName}] {ErrorMessage}", prefix,
                    handlerName, ex.Message);

                throw;
            }
        }
    }
}
