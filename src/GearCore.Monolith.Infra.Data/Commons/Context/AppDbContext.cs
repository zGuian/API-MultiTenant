using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.Infra.Data.Commons.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options
        , ITenantProvider tenantProvider = null!) : DbContext(options)
    {
        private readonly ITenantProvider? _tenantProvider = tenantProvider;

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantUser> TenantUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMoviments> StockMoviments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
