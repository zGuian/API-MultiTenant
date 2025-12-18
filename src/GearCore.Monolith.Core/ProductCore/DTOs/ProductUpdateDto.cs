using System.Text.Json.Serialization;

namespace GearCore.Monolith.Core.ProductCore.DTOs
{
    public class ProductUpdateDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime UpdateBy { get; set; } = DateTimeOffset.UtcNow.LocalDateTime;
    }
}
