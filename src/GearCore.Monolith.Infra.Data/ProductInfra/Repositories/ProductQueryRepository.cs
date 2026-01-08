using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Repositories
{
    public class ProductQueryRepository(AppDbContext context
        , ITenantProvider tenantProvider)
        : BaseQueryRepository<Product, string>(context, tenantProvider)
        , IProductQueryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            var count = await _context.Products.Where(p => p.Tenant.Id == _tenantProvider.TenantId)
                                               .CountAsync(ct);
            return count;
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct = default)
        {
            var products = await _context.Products.AsNoTracking()
                                                  .Where(p => p.Tenant.Id == _tenantProvider.TenantId)
                                                  .OrderBy(p => p.CreatedAt)
                                                  .Skip((pageIndex - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync(ct);
            return products;
        }
    }
}
