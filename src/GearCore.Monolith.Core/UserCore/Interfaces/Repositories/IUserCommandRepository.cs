using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.UserCore.Interfaces.Repositories
{
    public interface IUserCommandRepository : IBaseCommandRepository<User, string>
    {
    }
}
