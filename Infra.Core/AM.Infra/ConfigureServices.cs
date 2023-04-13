using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AM.Infra.Bus;
using AM.Infra.Logging;
using AM.Infra.Swagger;
using AM.Infra.TransactionalOutbox;
using AM.Infra.Validator;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Infra
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraCore(this IServiceCollection services, IConfiguration config,
            Type apiAnchorType, Action<IServiceCollection> doMoreActions = null)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("api", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] { apiAnchorType.Assembly });
            services.AddCustomValidators(new[] { apiAnchorType });
            services.AddDaprClient();
            services.AddControllers().AddMessageBroker(config);
            services.AddTransactionalOutbox(config);
            services.AddSwagger(apiAnchorType);

            doMoreActions?.Invoke(services);

            return services;
        }

        [DebuggerStepThrough]
        public static IServiceCollection AddCustomMediatR(
            this IServiceCollection services,
            Assembly[] assemblies = null,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddHttpContextAccessor();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            doMoreActions?.Invoke(services);

            return services;
        }
    }
}