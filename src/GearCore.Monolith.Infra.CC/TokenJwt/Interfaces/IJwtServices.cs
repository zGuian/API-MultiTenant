namespace GearCore.Monolith.Infra.CC.TokenJwt.Interfaces
{
    public interface IJwtServices
    {
        string GenerateToken(IApplicationUser user, IList<string> roles);
    }
}
