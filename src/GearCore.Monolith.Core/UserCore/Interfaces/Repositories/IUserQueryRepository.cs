using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.UserCore.Interfaces.Repositories
{
    public interface IUserQueryRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByNameAsync(string name);
        Task<IEnumerable<User>> GetPageAsync(int page, int pageSize);
        Task<IList<string>> GetRolesAsync(string userID);
    }
}
