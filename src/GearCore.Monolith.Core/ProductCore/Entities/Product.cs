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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; } = DateTimeOffset.Now.LocalDateTime;

        public virtual ICollection<StockMoviments> Moviments { get; set; } = [];
        public virtual Stock Stock { get; set; } = null!;
    }
}
