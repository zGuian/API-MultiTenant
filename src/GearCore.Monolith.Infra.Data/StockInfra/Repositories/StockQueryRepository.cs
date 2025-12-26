using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.StockInfra.Repositories
{
    public class StockQueryRepository(AppDbContext context
        , ILogger<StockQueryRepository> logger
        , ITenantProvider tenantProvider)
        : BaseQueryRepository<Stock, string>(context, tenantProvider), IStockQueryRepository
    {
        private readonly ILogger<StockQueryRepository> _logger = logger;
        private readonly AppDbContext _context = context;

        
    }
}
