using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Interfaces.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.TenantCore.Services
{
    public class TenantQuery(ITenantQueryRepository tenantQueryRepository) : ITenantQuery
    {
        private readonly ITenantQueryRepository _queryRepository = tenantQueryRepository;

        public async Task<HashSet<TenantDto?>> GetAll()
        {
            var models = await _queryRepository.GetAllToHashSet();
            if (models is HashSet<ITenantModel> && models is not null)
            {
                var dto = models.Adapt<HashSet<TenantDto?>>();
                return dto;
            }
            throw new NotImplementedException();
        }
    }
}
