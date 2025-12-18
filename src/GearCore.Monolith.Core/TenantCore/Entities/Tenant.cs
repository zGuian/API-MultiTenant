using GearCore.Monolith.Core.ProductCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Entities
{
    public class Tenant
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Subdomain { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<TenantUser> TenantUsers { get; set; } = [];
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
