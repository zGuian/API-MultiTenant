namespace GearCore.Monolith.Core.TenantCore.DTOs
{
    public class TenantViewDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Subdomain { get; set; }
        public bool IsActive { get; set; }
    }
}
