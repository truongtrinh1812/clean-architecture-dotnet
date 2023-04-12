using AM.Core.Domain.Events;
using AM.Infra.TransactionalOutbox.Dapr;
using AM.Infra.TransactionalOutbox.Dapr.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Infra.TransactionalOutbox
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddTransactionalOutbox(this IServiceCollection services, IConfiguration config, string provider = "dapr")
        {
            switch (provider)
            {
                case "dapr":
                    {
                        services.Configure<DaprTransactionalOutboxOptions>(config.GetSection(DaprTransactionalOutboxOptions.Name));
                        services.AddScoped<INotificationHandler<EventWrapper>, LocalDispatchedHandler>();
                        services.AddScoped<ITransactionalOutboxProcessor, TransactionalOutboxProcessor>();
                        break;
                    }
            }

            return services;
        }
    }
}