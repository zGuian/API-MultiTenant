using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
    {        
        [Column("COL_TENANT_ID")]
        public string TenantId { get; set; } = default!;
    }
}
