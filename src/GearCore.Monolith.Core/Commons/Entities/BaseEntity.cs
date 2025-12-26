using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Core.Commons.Entities
{
    public abstract class BaseEntity : ITenantProvider
    {
        [Column("COL_ID", Order = 0)]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();

        public virtual string TenantId { get; set; } = default!;

        [Column("COL_CREATE_AT")]
        public DateTime CreatedAt { get; set; }

        //[Column("COL_CREATED_BY")]
        //public string CreatedBy { get; set; } = string.Empty;

        [Column("COL_UPDATE_AT")]
        public DateTime UpdatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;

        //[Column("COL_UPDATE_BY")]
        //public string UpdateBy { get; set; } = string.Empty;
    }
}
