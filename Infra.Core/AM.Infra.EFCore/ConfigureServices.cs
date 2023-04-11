using AM.Core.Domain.Events;
using AM.Core.Repositories;
using AM.Infra.EFCore.Internal;
using AM.Infra.EFCore.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AM.Infra.EFCore
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services,
            string connString, Action<DbContextOptionsBuilder> doMoreDbContextOptionsConfigure = null,
            Action<IServiceCollection> doMoreActions = null)
                where TDbContext : DbContext, IDbFacadeResolver, IDomainEventContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                options.UseNpgsql(connString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }).UseSnakeCaseNamingConvention();

                doMoreDbContextOptionsConfigure?.Invoke(options);
            });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<TDbContext>());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>));

            services.AddHostedService<DbContextMigratorHostedService>();

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(repoType)
                .AddClasses(classes =>
                    classes.AssignableTo(repoType)).As(typeof(IRepository<>)).WithScopedLifetime()
                .AddClasses(classes =>
                    classes.AssignableTo(repoType)).As(typeof(IGridRepository<>)).WithScopedLifetime()
            );

            return services;
        }
    }
}
