using GearCore.Monolith.Core.StockCore.Entities.Enums;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class StockMoviments
    {
        public string Id { get; set; } = default!;
        public StockMovementType Type { get; set; }
        public decimal Quantity { get; set; } // + ou -
        public string Reason { get; set; } = default!;
        public string ReferenceId { get; set; } = default!; //SaleId ou SaleItemId
        public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;

        public string StockId { get; set; } = default!;
        public virtual ICollection<Stock> Stocks { get; set; } = [];

        public StockMoviments() { }
    }
}
