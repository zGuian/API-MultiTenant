using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.TenantCore.Services
{
    public class TenantQuery(ITenantQueryRepository tenantQueryRepository) : ITenantQuery
    {
        private readonly ITenantQueryRepository _queryRepository = tenantQueryRepository;

        public async Task<HashSet<TenantDto?>> GetAllAsync(CancellationToken ct = default)
        {
            var models = await _queryRepository.GetAllToHashSet(ct);
            if (models is not null)
            {
                var dto = models.Adapt<HashSet<TenantDto?>>();
                return dto;
            }
            throw new NotImplementedException();
        }

        public async Task<TenantDto> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var tenant = await _queryRepository.GetByIdAsync(id, ct);
            return tenant.Adapt<TenantDto>();
        }
    }
}
