using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class Stock : BaseEntity
    {
        public override string TenantId { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;

        public string ProductId { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;

        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } = 0;
        public bool Active { get; set; } = true;

        public virtual ICollection<StockMoviments> StockMoviments { get; set; } = [];

        public void SetValuesToEntity(string tenantId, string productId)
        {
            TenantId ??= tenantId;
            Tenant ??= new Tenant
            {
                Id = tenantId,
            };
            ProductId ??= productId;
            Product ??= new Product
            {
                Id = productId,
                TenantID = tenantId,
                Tenant = Tenant
            };
        }
    }
}
