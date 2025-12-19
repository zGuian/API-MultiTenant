using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Repositories
{
    public interface ITenantCommandRepository : IBaseCommandRepository<Tenant, string>
    {
        Task LinkToUserAsync(User user, string tenantID, CancellationToken ct = default);
        Task LinkToUserAsync(string userID, CancellationToken ct = default);
        Task RegisterAsync(Tenant entity, CancellationToken ct = default);
    }
}
