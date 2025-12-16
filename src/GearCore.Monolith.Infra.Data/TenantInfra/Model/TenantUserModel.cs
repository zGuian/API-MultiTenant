using GearCore.Monolith.Infra.Data.IdentityEF.Entities;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Model
{
    public class TenantUserModel
    {
        public string TenantId { get; set; } = default!;
        public TenantModel Tenant { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public ApplicationUser User = default!;

        public string Role { get; set; } = "ADM_SOFTWARE";
        public bool IsActive { get; set; } = default!;

        public DateTime CreatedBy { get; set; } = DateTimeOffset.Now.Date;
    }
}
