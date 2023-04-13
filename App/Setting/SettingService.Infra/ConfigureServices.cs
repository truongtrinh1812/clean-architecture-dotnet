using AM.Infra;
using AM.Infra.EFCore;
using AM.Infra.Swagger;
using AM.Infra.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SettingService.Infra.Data;
using SettingCoreAnchor = SettingService.Core.Anchor;

namespace SettingService.Infra
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
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] { typeof(SettingCoreAnchor).Assembly });
            services.AddCustomValidators(new[] { typeof(SettingCoreAnchor) });
            services.AddControllers();
            services.AddSwagger(apiType);

            // services.AddPostgresDbContext<MainDbContext>(
            //     config.GetConnectionString(DbName),
            //     dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
            //     svc => svc.AddRepository(typeof(Repository<>)));

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
                endpoints.MapDefaultControllerRoute();
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}