using GearCore.Monolith.Core.StockCore.Entities.Enums;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class StockMoviments
    {
        public string Id { get; set; } = default!;
        public StockMovementType Type { get; set; }
        public int Quantity { get; set; } // + ou -
        public string Reason { get; set; } = default!;
        public string ReferenceId { get; set; } = default!; //SaleId ou SaleItemId
        public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;

        public string StocksId { get; set; } = default!;
        public virtual Stock Stocks { get; set; } = new();

        public StockMoviments() { }
    }
}
