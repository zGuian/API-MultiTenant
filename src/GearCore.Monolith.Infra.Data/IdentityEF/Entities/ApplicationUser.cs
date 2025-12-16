using GearCore.Monolith.Core.UserCore.Interfaces.Entities;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.Infra.Data.TenantInfra.Model;
using Microsoft.AspNetCore.Identity;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Entities
{
    public class ApplicationUser : IdentityUser<string>, IApplicationUser
    {
        public virtual ICollection<TenantUserModel> TenantUsers { get; set; } = [];
    }
}
