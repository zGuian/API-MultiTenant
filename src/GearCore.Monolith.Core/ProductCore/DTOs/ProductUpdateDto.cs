namespace GearCore.Monolith.Core.ProductCore.DTOs
{
    public class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string Brand { get; set; } = string.Empty;
    }
}
