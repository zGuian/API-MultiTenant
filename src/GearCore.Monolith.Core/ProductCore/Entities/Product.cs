using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.ProductCore.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string? Brand { get; set; } = string.Empty;
        public virtual Tenant Tenant { get; set; } = new();
        public string TenantID { get; set; } = string.Empty;

        public Product()
        { }

        public Product(string name, string? description, decimal price, bool isActive, string? brand)
        {
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
            Brand = brand;
            UpdatedBy = DateTimeOffset.UtcNow.LocalDateTime;
        }
    }
}
