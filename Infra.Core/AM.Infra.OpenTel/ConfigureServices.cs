using AM.Infra.OpenTel.MediatR;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Exporter;
using OpenTelemetry.Trace;

namespace AM.Infra.OpenTel
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddCustomOtelWithZipkin(this IServiceCollection services,
           IConfiguration config, Action<ZipkinExporterOptions> configureZipkin = null)
        {
            services.AddOpenTelemetryTracing(b => b
                .SetSampler(new AlwaysOnSampler())
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddGrpcClientInstrumentation()
                .AddSqlClientInstrumentation(o => o.SetDbStatementForText = true)
                .AddSource(OpenTelMediatROptions.OpenTelMediatRName)
                .AddZipkinExporter(o =>
                {
                    config.Bind("OpenTelZipkin", o);
                    configureZipkin?.Invoke(o);
                })
                .Build());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(OpenTelMediatRTracingBehavior<,>));

            return services;
        }
    }
}