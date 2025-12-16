namespace GearCore.Monolith.Core.TenantCore.DTOs
{
    public class TenantRegisterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
