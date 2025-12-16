using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.ProductCore.Entities;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class Stock : BaseEntity
    {
        public int CurrentQuantity { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = new();

        public Stock() { }
    }
}
