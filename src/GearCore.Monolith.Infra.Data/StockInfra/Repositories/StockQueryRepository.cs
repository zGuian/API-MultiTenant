using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.StockInfra.Repositories
{
    public class StockQueryRepository(AppDbContext context
        , ILogger<StockQueryRepository> logger
        , ITenantProvider tenantProvider)
        : BaseQueryRepository<Stock, string>(context, tenantProvider), IStockQueryRepository
    {
        private readonly ILogger<StockQueryRepository> _logger = logger;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly AppDbContext _context = context;

        public override async Task<HashSet<Stock>> GetAllAsync(CancellationToken ct = default)
        {
            var result = await _context.Stocks.Where(s => s.TenantId == _tenantProvider.TenantId)
                                 .OrderByDescending(s => s.Quantity)
                                 .Select(s => new Stock
                                 {
                                     Id = s.Id,
                                     TenantId = s.Tenant.Id,
                                     Product = new Product
                                     {
                                         Id = s.Product.Id,
                                         Name = s.Product.Name,
                                         Description = s.Product.Description,
                                         Price = s.Product.Price,
                                         CreatedAt = s.Product.CreatedAt
                                     },
                                     Quantity = s.Quantity,
                                     ReservedQuantity = s.ReservedQuantity,
                                     Active = s.Active,
                                     UpdatedAt = s.UpdatedAt
                                 })
                                 .ToHashSetAsync(ct);
            return result;
        }
    }
}
