
using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Repositories
{
    public interface ITenantQueryRepository : IBaseQueryRepository<Tenant, string>
    {
        Task<HashSet<Tenant>> GetAllToHashSet(CancellationToken ct = default);
    }
}
