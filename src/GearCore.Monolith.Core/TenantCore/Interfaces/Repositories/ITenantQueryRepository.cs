using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Repositories
{
    public interface ITenantQueryRepository
    {
        Task<HashSet<Tenant>> GetAllToHashSet(CancellationToken ct = default);
        Task<Tenant> GetByIdAsync(string id, CancellationToken ct = default);
    }
}
