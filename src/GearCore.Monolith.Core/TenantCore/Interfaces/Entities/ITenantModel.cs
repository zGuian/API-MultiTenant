namespace GearCore.Monolith.Core.TenantCore.Interfaces.Entities
{
    public interface ITenantModel
    {
        bool Active { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        string? Subdomain { get; set; }
    }
}
