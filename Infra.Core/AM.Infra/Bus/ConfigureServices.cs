using AM.Infra.Bus.Dapr;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Infra.Bus
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddMessageBroker(this IMvcBuilder mvcBuilder,
            IConfiguration config,
            string messageBrokerType = "dapr")
        {
            switch (messageBrokerType)
            {
                case "dapr":
                    mvcBuilder.Services.Configure<DaprEventBusOptions>(config.GetSection(DaprEventBusOptions.Name));
                    mvcBuilder.AddDapr();
                    mvcBuilder.Services.AddScoped<IEventBus, DaprEventBus>();
                    break;
            }

            return mvcBuilder.Services;
        }
    }
}