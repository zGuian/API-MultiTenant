using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Repositories
{
    public class TenantQueryRepository(AppDbContext context
        , ILogger<TenantQueryRepository> logger) 
        : BaseQueryRepository<Tenant, string>(context), ITenantQueryRepository
    {
        private readonly ILogger<TenantQueryRepository> _logger = logger;
        private readonly AppDbContext _context = context;

        public async Task<HashSet<ITenantModel>> GetAllToHashSet()
        {
            var tenants = await _context.Tenants.ToHashSetAsync();
            return tenants.Cast<ITenantModel>().ToHashSet();
        }
    }
}
