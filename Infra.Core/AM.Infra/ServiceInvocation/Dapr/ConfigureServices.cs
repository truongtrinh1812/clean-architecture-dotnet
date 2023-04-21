using System.Text.Json;
using Dapr.Client;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using RestEase.HttpClientFactory;

namespace AM.Infra.ServiceInvocation.Dapr
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDaprClient(this IServiceCollection services)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, PropertyNameCaseInsensitive = true,
            };

            services.AddSingleton(options);

            services.AddDaprClient(client =>
            {
                client.UseJsonSerializationOptions(options);
            });

            return services;
        }

        public static IServiceCollection AddRestClient<T>(this IServiceCollection services,
            Type httpClientApi, string appName = "localhost", int appPort = 5000) where T: DelegatingHandler
        {
            var appUri = $"http://{appName}:{appPort}";

            services.AddScoped<T>();
            services.AddRestEaseClient(httpClientApi, appUri, client =>
            {
                client.RequestPathParamSerializer = new StringEnumRequestPathParamSerializer();
            }).AddHttpMessageHandler<T>();

            return services;
        }
    }
}