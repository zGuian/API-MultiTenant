
using GearCore.Monolith.Core.TenantCore.Interfaces.Entities;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Repositories
{
    public interface ITenantQueryRepository
    {
        Task<HashSet<ITenantModel>> GetAllToHashSet();
    }
}
