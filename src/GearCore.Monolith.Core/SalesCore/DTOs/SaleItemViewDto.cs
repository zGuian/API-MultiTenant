using GearCore.Monolith.Core.ProductCore.DTOs;

namespace GearCore.Monolith.Core.SalesCore.DTOs
{
    public class SaleItemViewDto
    {
        public string Id { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductViewDto Product { get; set; } = new();
    }
}
