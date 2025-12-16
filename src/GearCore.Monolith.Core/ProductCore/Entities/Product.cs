using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.StockCore.Entities;

namespace GearCore.Monolith.Core.ProductCore.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; } = string.Empty;
        public virtual ICollection<StockMoviments> Moviments { get; set; } = [];
        public virtual Stock Stock { get; set; } = null!;
    }
}
