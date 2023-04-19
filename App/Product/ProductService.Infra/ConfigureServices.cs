using AM.Infra;
using AM.Infra.Bus;
using AM.Infra.EFCore;
using AM.Infra.Swagger;
using AM.Infra.TransactionalOutbox;
using AM.Infra.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProductService.Infra.Data;
using ProductCoreAnchor = ProductService.Core.Anchor;

namespace ProductService.Infra
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
            services.AddCustomMediatR(new[] { typeof(ProductCoreAnchor).Assembly });
            services.AddCustomValidators(new[] { typeof(ProductCoreAnchor) });
            services.AddDaprClient();
            services.AddControllers().AddMessageBroker(config);
            services.AddTransactionalOutbox(config);
            services.AddSwagger(apiType);

            services.AddPostgresDbContext<MainDbContext>(
                config.GetConnectionString(DbName),
                dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
                svc => svc.AddRepository(typeof(Repository<>))
                );

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = config.GetValue<string>("Identity:Authority");
                     options.Audience = "product";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiCaller", policy =>
                {
                    policy.RequireClaim("scope", "product");
                });

                options.AddPolicy("RequireInteractiveUser", policy =>
                {
                    policy.RequireClaim("sub");
                });
            });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCloudEvents();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();
                endpoints.MapControllers()
                    .RequireAuthorization("ApiCaller");
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}