using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.StockInfra.Repositories
{
    public class StockQueryRepository(AppDbContext context
        , ILogger<StockQueryRepository> logger)
        : BaseQueryRepository<Stock, Guid>(context), IStockQueryRepository
    {
        private readonly ILogger<StockQueryRepository> _logger = logger;
        private readonly AppDbContext _context = context;
    }
}
