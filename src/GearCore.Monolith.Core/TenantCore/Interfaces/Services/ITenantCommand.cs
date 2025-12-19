
using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Services
{
    public interface ITenantCommand
    {
        Task LinkToUserAsync(User user, string tenantID, CancellationToken ct = default);
        Task LinkToUserAsync(string userID, CancellationToken ct = default);
        Task RegisterAsync(TenantRegisterDto dto, CancellationToken ct = default);
    }
}
