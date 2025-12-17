using GearCore.Monolith.Core.UserCore.DTOs;

namespace GearCore.Monolith.Core.UserCore.Interfaces.Services
{
    public interface IUserCommand
    {
        Task<bool> CreateAsync(UserRegisterDto dto, string tenantID, CancellationToken ct = default);

    }
}
