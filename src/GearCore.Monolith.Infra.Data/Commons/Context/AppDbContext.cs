using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Infra.CC.Tenacy.Interfaces;
using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using GearCore.Monolith.Infra.Data.TenantInfra.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GearCore.Monolith.Infra.Data.Commons.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options
        , ITenantProvider tenantProvider = null!)
        : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        private readonly ITenantProvider? _tenantProvider = tenantProvider;

        public DbSet<TenantModel> Tenants { get; set; } = null!;
        public DbSet<TenantUserModel> TenantUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyMultiTenantFilters(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetTenantIdOnNewEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTenantIdOnNewEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTenantIdOnNewEntities()
        {
            if (_tenantProvider == null) return;

            var tenantId = _tenantProvider.TenantId;

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                if (entry.Entity.GetType().GetProperty("TenantId") != null)
                {
                    entry.CurrentValues["TenantId"] = tenantId;
                }
            }
        }

        private void ApplyMultiTenantFilters(ModelBuilder modelBuilder)
        {
            if (_tenantProvider == null) return;

            var tenantId = _tenantProvider.TenantId;

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetProperty(nameof(BaseEntity.TenantId)) != null)
                {
                    var method = typeof(AppDbContext)
                        .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                        !.MakeGenericMethod(entityType.ClrType);

                    method.Invoke(null, [modelBuilder, tenantId]);
                }
            }
        }

        private static void SetGlobalQueryFilter<TEntity>(ModelBuilder builder, string tenantId)
            where TEntity : class
        {
            builder.Entity<TEntity>()
                .HasQueryFilter(e => EF.Property<string>(e, "TenantId") == tenantId);
        }
    }
}
