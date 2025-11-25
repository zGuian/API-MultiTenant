namespace GearCore.Monolith.Core.Commons.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public string TenantId { get; set; } = default!;
    }
}
