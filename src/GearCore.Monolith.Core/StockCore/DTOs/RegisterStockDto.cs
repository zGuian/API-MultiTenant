using System.Text.Json.Serialization;

namespace GearCore.Monolith.Core.StockCore.DTOs
{
    public class RegisterStockDto
    {
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Active { get; set; } = true;

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;
    }
}
