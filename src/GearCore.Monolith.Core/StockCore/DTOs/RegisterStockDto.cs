namespace GearCore.Monolith.Core.StockCore.DTOs
{
    public class RegisterStockDto
    {
        public string ProductId { get; set; } = default!;
        public decimal Quantity { get; set; }
        public bool Active { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;
        public DateTime UpdatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;
    }
}
