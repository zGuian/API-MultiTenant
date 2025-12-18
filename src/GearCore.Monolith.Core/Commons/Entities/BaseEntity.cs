using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Core.Commons.Entities
{
    public abstract class BaseEntity
    {
        [Column("COL_ID", Order = 0)]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString();

        [Column("COL_CREATE_BY")]
        public DateTime CreatedBy { get; set; }

        [Column("COL_UPDATE_BY")]
        public DateTime UpdatedBy { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;
    }
}
