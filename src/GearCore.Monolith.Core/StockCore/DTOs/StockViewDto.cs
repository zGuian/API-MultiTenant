using GearCore.Monolith.Core.ProductCore.DTOs;

namespace GearCore.Monolith.Core.StockCore.DTOs
{
    public class StockViewDto
    {
        public string Id { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public ProductViewDto Product { get; set; } = new();
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } = 0;
        public bool Active { get; set; } = true;
        public DateTime UpdatedAt { get; set; }

        public StockViewDto()
        { }

        //public StockViewDto(string tenantId, string productId, int quantity, int reservedQuantity, bool active)
        //{
        //    TenantId = tenantId;
        //    ProductId = productId;
        //    Quantity = quantity;
        //    ReservedQuantity = reservedQuantity;
        //    Active = active;
        //}
    }
}
