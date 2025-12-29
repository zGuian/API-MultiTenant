using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.Data.Commons.TokenJwt.Interfaces
{
    public interface IJwtServices
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
