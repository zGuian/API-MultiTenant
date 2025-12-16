namespace GearCore.Monolith.Core.TenantCore.DTOs
{
    public class TenantDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Subdomain { get; set; }
        public bool Active { get; set; }
    }
}
