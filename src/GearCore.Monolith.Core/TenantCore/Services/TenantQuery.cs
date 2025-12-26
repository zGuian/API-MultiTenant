using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.TenantCore.Services
{
    public class TenantQuery(ITenantQueryRepository tenantQueryRepository) : ITenantQuery
    {
        private readonly ITenantQueryRepository _queryRepository = tenantQueryRepository;

        public async Task<HashSet<TenantViewDto?>> GetAllAsync(CancellationToken ct = default)
        {
            var models = await _queryRepository.GetAllToHashSet(ct);
            return models.Adapt<HashSet<TenantViewDto?>>();
        }

        public async Task<TenantViewDto> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var tenant = await _queryRepository.GetByIdAsync(id, ct);
            return tenant.Adapt<TenantViewDto>();
        }
    }
}
