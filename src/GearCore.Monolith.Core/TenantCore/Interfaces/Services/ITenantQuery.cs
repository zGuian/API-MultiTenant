
using GearCore.Monolith.Core.TenantCore.DTOs;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Services
{
    public interface ITenantQuery 
    {
        Task<HashSet<TenantViewDto?>> GetAllAsync(CancellationToken ct = default);
        Task<TenantViewDto> GetByIdAsync(string id, CancellationToken ct = default);
    }
}
