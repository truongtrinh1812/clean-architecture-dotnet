using AM.Infra;
using AM.Infra.Bus;
using AM.Infra.ServiceInvocation.Dapr;
using AM.Infra.Swagger;
using AM.Infra.TransactionalOutbox;
using AM.Infra.Validator;
using AppContracts;
using AppContracts.RestApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomerCoreAnchor = CustomerService.Core.Anchor;


namespace CustomerService.Infra
{
    public static class ConfigureServices
    {
        private const string CorsName = "api";
        private const string DbName = "postgres";
        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env, Type apiType)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] { typeof(CustomerCoreAnchor) });
            services.AddCustomValidators(new[] { typeof(CustomerCoreAnchor) });
            services.AddDaprClient();
            services.AddControllers().AddMessageBroker(config);
            services.AddTransactionalOutbox(config);
            services.AddSwagger(apiType);

            // services.AddPostgresDbContext<MainDbContext>(
            //     config.GetConnectionString(DbName),
            //     dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
            //     svc => svc.AddRepository(typeof(Repository<>)));

            services.AddRestClient(typeof(ICountryApi), AppConstants.SettingAppName,
                config.GetValue("Services:SettingApp:Port", 5005));

            return services;
        }

        public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsName);
            app.UseRouting();
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapDefaultControllerRoute();
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}