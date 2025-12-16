using GearCore.Monolith.Core.TenantCore.Interfaces.Entities;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Model
{
    public class TenantModel : ITenantModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Subdomain { get; set; }
        public bool Active { get; set; } = true;
        public virtual ICollection<TenantUserModel> TenantUsers { get; set; } = [];
    }
}
