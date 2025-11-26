namespace GearCore.Monolith.Infra.CC.TokenJwt.Interfaces
{
    public interface IApplicationUser
    {
        string TenantId { get; set; }
        Guid Id { get; set; }
        string? UserName { get; set; }
        string? Email { get; set; }
    }
}
