using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Repositories
{
    public class SalesQueryRepository(AppDbContext context
        , ILogger<SalesQueryRepository> logger)
        : BaseQueryRepository<Sales, string>(context)
        , ISalesQueryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<SalesQueryRepository> _logger = logger;
    }
}
