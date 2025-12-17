using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.Commons.Utils.Converters;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using GearCore.Monolith.Core.ProductCore.Services;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using GearCore.Monolith.Core.StockCore.Services;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.Core.TenantCore.Services;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Core.UserCore.Services;
using GearCore.Monolith.Infra.CC.Tenacy;
using GearCore.Monolith.Infra.CC.Tenacy.Interfaces;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using GearCore.Monolith.Infra.Data.Commons.TokenJwt;
using GearCore.Monolith.Infra.Data.ProductInfra.Repositories;
using GearCore.Monolith.Infra.Data.StockInfra.Repositories;
using GearCore.Monolith.Infra.Data.TenantInfra.Repositories;
using GearCore.Monolith.Infra.Data.UserInfra.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
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
            AddMapsterDependecies(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserCommand, UserCommand>();
            services.AddScoped<IUserQuery, UserQuery>();

            services.AddScoped<ITenantCommand, TenantCommand>();
            services.AddScoped<ITenantQuery, TenantQuery>();

            services.AddScoped<IStockCommand, StockCommand>();
            services.AddScoped<IStockQuery, StockQuery>();

            services.AddScoped<IProductCommand, ProductCommand>();
            services.AddScoped<IProductQuery, ProductQuery>();
        }

        private static void AddMapsterDependecies(this IServiceCollection services)
        {
            services.AddMapster();
            MapsterConfig.Configure();
        }
        #endregion Core

        #region Infrastructure        
        private static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            AddRepositories(services);
            AddServicesCrossCutting(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();

            services.AddScoped<ITenantCommandRepository, TenantCommandRepository>();
            services.AddScoped<ITenantQueryRepository, TenantQueryRepository>();

            services.AddScoped<IStockCommandRepository, StockCommandRepository>();
            services.AddScoped<IStockQueryRepository, StockQueryRepository>();

            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((sp, opts) =>
            {
                var httpTenant = sp.GetService<ITenantProvider>();

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

        private static void AddServicesCrossCutting(this IServiceCollection services)
        {
            services.AddScoped<ITenantProvider, HttpTenantProvider>();
            services.AddScoped<IJwtServices, JwtServices>();
        }
        #endregion Infrastructure
    }
}
