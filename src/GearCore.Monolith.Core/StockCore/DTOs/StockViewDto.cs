namespace GearCore.Monolith.Core.StockCore.DTOs
{
    public class StockViewDto
    {
        public string TenantId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; } = 0;
        public bool Active { get; set; } = true;

        public StockViewDto()
        { }

        public StockViewDto(string tenantId, string productId, int quantity, int reservedQuantity, bool active)
        {
            TenantId = tenantId;
            ProductId = productId;
            Quantity = quantity;
            ReservedQuantity = reservedQuantity;
            Active = active;
        }
    }
}
