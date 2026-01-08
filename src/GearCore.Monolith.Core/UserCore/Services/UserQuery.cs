using GearCore.Monolith.Core.Commons.Security;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.UserCore.Services
{
    public class UserQuery(IUserQueryRepository queryRepository) : IUserQuery
    {
        private readonly IUserQueryRepository _queryRepository = queryRepository;

        public bool CheckPasswordSignIn(User user, string password)
            => SecurityServices.VerifyPassword(password, user.PasswordHash);

        public async Task<User?> FindByEmailAsync(string email)
            => await _queryRepository.FindByEmailAsync(email);

        public async Task<User?> FindByNameAsync(string name)
            => await _queryRepository.FindByNameAsync(name);

        public async Task<IEnumerable<UserDto>> GetPageAsync(int pageIndex, int pageSize)
        {
            var users = await _queryRepository.GetPageAsync(pageIndex, pageSize);
            return users.Adapt<IEnumerable<UserDto>>();
        }

        public async Task<IList<string>> GetRolesAsync(User user) =>
            await _queryRepository.GetRolesAsync(user.Id);
    }
}
