using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.ProductCore.Entities;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class StockMoviments : BaseEntity
    {
        public int Quantity { get; set; }
        public int PreviousQuantity { get; set; }
        public int NewQuantity { get; set; }

        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string? Observation { get; set; } = string.Empty;

        public virtual Product Product { get; set; } = new();
        public Guid ProductId { get; set; }

        public StockMoviments() { }
    }
}
