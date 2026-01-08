using GearCore.Monolith.Core.TenantCore.Entities.Enums;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Entities
{
    public class TenantUser
    {
        public string TenantId { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;

        public string UserId { get; set; } = default!;
        public User User = default!;

        public string Role { get; set; } = nameof(RoleTenantEnum.SYSTEM_USER);
        public bool IsActive { get; set; } = true;

        public DateTime CreatedBy { get; set; } = DateTimeOffset.Now.Date;

        public TenantUser()
        {
        }

        public TenantUser(User user, Tenant tenant)
        {
            TenantId = tenant.Id;
            Tenant = tenant;
            UserId = user.Id;
            User = user;
        }
    }
}
