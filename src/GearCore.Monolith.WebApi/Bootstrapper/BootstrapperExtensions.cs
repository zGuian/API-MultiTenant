using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using GearCore.Monolith.Core.ProductCore.Services;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using GearCore.Monolith.Core.StockCore.Services;
using GearCore.Monolith.Infra.CC.Tenacy;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using GearCore.Monolith.Infra.Data.ProductInfra.Repositories;
using GearCore.Monolith.Infra.Data.StockInfra.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.WebApi.Bootstrapper
{
    public static class BootstrapperExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreDependencies();
            services.AddInfrastructureDependencies(configuration);

            services.AddMapster();

            return services;
        }

        #region Core
        private static void AddCoreDependencies(this IServiceCollection services)
        {
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IStockCommand, StockCommand>();
            services.AddScoped<IStockQuery, StockQuery>();

            services.AddScoped<IProductCommand, ProductCommand>();
            services.AddScoped<IProductQuery, ProductQuery>();
        }
        #endregion Core

        #region Infrastructure        
        private static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            AddRepositories(services);

            services.AddScoped<ITenantProvider, HttpTenantProvider>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IStockCommandRepository, StockCommandRepository>();
            services.AddScoped<IStockQueryRepository, StockQueryRepository>();

            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("SQLDefault"), config =>
                {
                    config.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    config.CommandTimeout(60);
                    config.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
            });
        }
        #endregion Infrastructure
    }
}
