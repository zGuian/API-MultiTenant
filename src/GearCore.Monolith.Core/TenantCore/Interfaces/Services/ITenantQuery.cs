
using GearCore.Monolith.Core.TenantCore.DTOs;

namespace GearCore.Monolith.Core.TenantCore.Interfaces.Services
{
    public interface ITenantQuery
    {
        Task<HashSet<TenantDto?>> GetAll();
    }
}
