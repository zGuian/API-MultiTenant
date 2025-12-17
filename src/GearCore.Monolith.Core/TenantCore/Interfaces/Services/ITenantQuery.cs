
using GearCore.Monolith.Core.TenantCore.DTOs;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Services
{
    public interface ITenantQuery 
    {
        Task<HashSet<TenantDto?>> GetAllAsync(CancellationToken ct = default);
        Task<TenantDto> GetByIdAsync(string id, CancellationToken ct = default);
    }
}
