using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Entities;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Repositories
{
    public interface ITenantCommandRepository
    {
        Task LinkToUserAsync(IApplicationUser user, string tenantID);
        Task RegisterAsync(Tenant entity);
    }
}
