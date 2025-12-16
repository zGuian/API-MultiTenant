
using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Services
{
    public interface ITenantCommand
    {
        Task LinkToUserAsync(IApplicationUser user, string tenantID);
        Task RegisterAsync(TenantRegisterDto dto);
    }
}
