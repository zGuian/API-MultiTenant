namespace GearCore.Monolith.Infra.CC.TokenJwt.Interfaces
{
    public interface IApplicationUser
    {
        string Id { get; set; }
        string? UserName { get; set; }
        string? Email { get; set; }
    }
}
