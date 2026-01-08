using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Repositories
{
    public class SalesCommandRepository(AppDbContext context
        , ILogger<SalesCommandRepository> logger)
        : BaseCommandRepository<Sales, string>(context)
        , ISalesCommandRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<SalesCommandRepository> _logger = logger;

        public async Task CompleteSale()
        {

        }
    }
}
