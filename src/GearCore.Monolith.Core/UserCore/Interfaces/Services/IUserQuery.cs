using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.UserCore.Interfaces.Services
{
    public interface IUserQuery
    {
        Task<IEnumerable<UserDto>> GetPageAsync(int page, int pageSize);
        Task<IList<string>> GetRolesAsync(User user);
        bool CheckPasswordSignIn(User user, string password);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByNameAsync(string name);
    }
}
