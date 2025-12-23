using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class Stock : BaseEntity
    {
        public string TenantId { get; set; } = default!;
        public Tenant Tenant { get; set; } = default!;

        public string ProductId { get; set; } = default!;
        public virtual Product Product { get; set; } = default!;

        public decimal Quantity { get; set; }
        public decimal ReservedQuantity { get; set; }
        public bool Active { get; set; } = true;

        public virtual StockMoviments StockMoviments { get; set; } = new();
        public string StockMovimentsId { get; set; } = default!;
    }
}
