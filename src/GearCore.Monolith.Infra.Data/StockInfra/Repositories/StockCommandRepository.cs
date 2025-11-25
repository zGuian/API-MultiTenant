using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.StockInfra.Repositories
{
    public class StockCommandRepository(AppDbContext context
        , ILogger<StockCommandRepository> logger) : BaseCommandRepository<Stock, Guid>(context)
        , IStockCommandRepository
    {
        private readonly ILogger<StockCommandRepository> _logger = logger;
    }
}