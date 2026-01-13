using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Repositories
{
    public class SalesQueryRepository(AppDbContext context
        , ILogger<SalesQueryRepository> logger
        , ITenantProvider tenantProvider)
        : BaseQueryRepository<Sales, string>(context, tenantProvider)
        , ISalesQueryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly ILogger<SalesQueryRepository> _logger = logger;

        public async Task<int> CountAsync(CancellationToken ct = default) => await _context.Sales.CountAsync(ct);

        public async Task<IEnumerable<Sales>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct = default)
        {
            var sales = await _context.Sales.AsNoTracking()
                                            .Where(s => s.Tenant.Id == _tenantProvider.TenantId)
                                            .OrderBy(s => s.CreatedAt)
                                            .Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize)
                                            .Select(s => new Sales
                                            {
                                                TotalAmount = s.TotalAmount,
                                                DiscountAmount = s.DiscountAmount,
                                                FinalAmount = s.FinalAmount,
                                                Status = s.Status,
                                                TenantId = s.TenantId,
                                                SaleItems = s.SaleItems.Select(si => new SaleItem
                                                {
                                                    Id = si.Id,
                                                    ProductName = si.ProductName,
                                                    UnitPrice = si.UnitPrice,
                                                    Quantity = si.Quantity,
                                                    TotalPrice = si.TotalPrice,
                                                    Product = new Product
                                                    {
                                                        Id = si.Product.Id,
                                                        Name = si.Product.Name,
                                                        Description = si.Product.Description,
                                                        Price = si.Product.Price,
                                                        CreatedAt = si.Product.CreatedAt
                                                    }
                                                }).ToList(),
                                                CreatedAt = s.CreatedAt,
                                                UpdatedAt = s.UpdatedAt
                                })
                                .ToListAsync(ct);
            return sales;
        }
    }
}
