using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.CC.TokenJwt.Interfaces
{
    public interface IJwtServices
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
