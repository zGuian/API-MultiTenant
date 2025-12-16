using GearCore.Monolith.Core.TenantCore.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Core.Commons.Entities
{
    public abstract class BaseEntity
    {
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();
        public virtual Tenant Tenant { get; set; } = new();

        [Column("COL_TENANT_ID")]
        public string TenantId { get; set; } = default!;

        [Column("COL_CREATE_BY")]
        public DateTime CreatedBy { get; set; }

        [Column("COL_UPDATE_BY")]
        public DateTime UpdatedBy { get; set; } = DateTimeOffset.Now.Date;

        [Column("COL_IS_DELETED")]
        public bool IsDeleted { get; set; }
    }
}
