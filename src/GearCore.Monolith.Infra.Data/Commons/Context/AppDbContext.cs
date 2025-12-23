using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Infra.CC.Tenacy.Interfaces;
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
            //ApplyMultiTenantFilters(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    SetTenantIdOnNewEntities();
        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    SetTenantIdOnNewEntities();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void SetTenantIdOnNewEntities()
        //{
        //    if (_tenantProvider == null) return;

        //    var tenantId = _tenantProvider.TenantID;

        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.State == EntityState.Added);

        //    foreach (var entry in entries)
        //    {
        //        if (entry.Entity.GetType().GetProperty("TenantID") != null)
        //        {
        //            entry.CurrentValues["TenantID"] = tenantId;
        //        }
        //    }
        //}

        //private void ApplyMultiTenantFilters(ModelBuilder modelBuilder)
        //{
        //    if (_tenantProvider == null) return;

        //    var tenantId = _tenantProvider.TenantID;

        //    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //    {
        //        if (entityType.ClrType.GetProperty(nameof(BaseEntity.TenantID)) != null)
        //        {
        //            var method = typeof(AppDbContext)
        //                .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
        //                !.MakeGenericMethod(entityType.ClrType);

        //            method.Invoke(null, [modelBuilder, tenantId]);
        //        }
        //    }
        //}

        //private static void SetGlobalQueryFilter<TEntity>(ModelBuilder builder, string tenantId)
        //    where TEntity : class
        //{
        //    builder.Entity<TEntity>()
        //        .HasQueryFilter(e => EF.Property<string>(e, "TenantID") == tenantId);
        //}
    }
}
