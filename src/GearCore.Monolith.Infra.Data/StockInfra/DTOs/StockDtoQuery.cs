namespace GearCore.Monolith.Infra.Data.StockInfra.DTOs
{
    internal class StockDtoQuery
    {
        public string Id { get; set; } = string.Empty;
        public string TenantName { get; set; } = string.Empty;
        public ProductDtoQuery Product { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public bool Active { get; set; }
        public DateTime UpdateAt { get; set; }
    }

    internal class ProductDtoQuery
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Brand { get; set; } = string.Empty;

    }
}
