using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Infra.Data.IdentityEF
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Column("COL_TENANT_ID")]
        public string TenantId { get; set; } = default!;
    }
}
