using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Repositories
{
    public class TenantQueryRepository(AppDbContext context
        , ILogger<TenantQueryRepository> logger) : ITenantQueryRepository
    {
        private readonly ILogger<TenantQueryRepository> _logger = logger;
        private readonly AppDbContext _context = context;

        public async Task<HashSet<Tenant>> GetAllToHashSet(CancellationToken ct = default)
            => await _context.Tenants.ToHashSetAsync(ct);

        public async Task<Tenant> GetByIdAsync(string id, CancellationToken ct = default)
            => await _context.Tenants.FirstOrDefaultAsync(e => e.Id == id, ct)
                ?? throw new NotFoundException("Entity not found");
    }
}
