using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Infra.Data.ProductInfra.Seed;
using GearCore.Monolith.Infra.Data.StockInfra.Seed;
using GearCore.Monolith.Infra.Data.TenantInfra.Seed;
using GearCore.Monolith.Infra.Data.UserInfra.Seed;

namespace GearCore.Monolith.Infra.Data.Commons.Context
{
    public static class SeedManualContext
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var tenants = await SeedTenantsAsync(context);
            var users = await SeedUsersAsync(context);
            var roles = await SeedRolesAsync(context);
            await SeedUserRolesAsync(context, users, roles);
            await SeedTenantUserAsync(context, tenants, users);
            var product = await SeedProductsAsync(context, tenants);
            await SeedStockAsync(context, product);
            await context.SaveChangesAsync();
        }

        private static async Task<IEnumerable<User>> SeedUsersAsync(AppDbContext context)
        {
            if (context.Users.Any())
                return [];

            var users = UserSeed.GetSeeds();
            await context.Users.AddRangeAsync(users);
            return users;
        }

        private static async Task<IEnumerable<Roles>> SeedRolesAsync(AppDbContext context)
        {
            if (context.Roles.Any())
                return [];

            var roles = RoleSeed.GetSeeds();
            await context.Roles.AddRangeAsync(roles);
            return roles;
        }

        private static async Task SeedUserRolesAsync(AppDbContext context, IEnumerable<User> users, IEnumerable<Roles> roles)
        {
            if (context.UserRoles.Any())
                return;

            var userRoles = UserRoleSeed.GetSeeds([.. users], [.. roles]);
            await context.UserRoles.AddRangeAsync(userRoles);
        }

        private static async Task<IEnumerable<Tenant>> SeedTenantsAsync(AppDbContext context)
        {
            if (context.Tenants.Any())
                return [];

            var tenants = TenantSeed.GetSeeds();
            await context.Tenants.AddRangeAsync(tenants);
            return tenants;
        }

        private static async Task SeedTenantUserAsync(AppDbContext context, IEnumerable<Tenant> tenants, IEnumerable<User> users)
        {
            if (context.TenantUsers.Any())
                return;

            var tenantsUser = TenantUserSeed.GetSeeds([.. tenants], [.. users]);
            await context.TenantUsers.AddRangeAsync(tenantsUser);
        }

        private static async Task<IEnumerable<Product>> SeedProductsAsync(AppDbContext context, IEnumerable<Tenant> tenants)
        {
            if (context.Products.Any())
                return [];

            var products = ProductSeed.GetSeeds([.. tenants]);
            foreach (var item in products)
            {
                var tenantId = item.TenantId;

                var exists = context.Products.Any(p =>
                    p.TenantId == tenantId &&
                    p.Id == item.Id
                );

                if (!exists)
                    await context.Products.AddAsync(item);
            }
            return products;
        }

        private static async Task SeedStockAsync(AppDbContext context, IEnumerable<Product> products)
        {
            if (context.Stocks.Any())
                return;

            var stocks = StockSeed.GetSeeds([.. products]);
            await context.Stocks.AddRangeAsync(stocks);
        }
    }
}
