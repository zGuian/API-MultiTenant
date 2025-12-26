using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using System.Xml.Linq;

namespace GearCore.Monolith.Core.ProductCore.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string? Brand { get; set; } = string.Empty;

        public virtual ICollection<Stock> Stocks { get; set; } = [];
        public virtual ICollection<SaleItem> SaleItems { get; set; } = [];

        public virtual Tenant Tenant { get; set; } = new();
        public override string TenantId { get; set; } = string.Empty;

        public Product()
        { }

        public Product(string tenantId, string name, string? description, decimal price, bool isActive,
            string? brand)
        {
            Id = Guid.NewGuid().ToString();
            TenantId = tenantId;
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
            Brand = brand;
            CreatedAt = DateTimeOffset.UtcNow.LocalDateTime;
            UpdatedAt = DateTimeOffset.UtcNow.LocalDateTime;
        }

        public Product(Tenant tenant, string name, string? description, decimal price, bool isActive,
            string? brand)
        {
            Id = Guid.NewGuid().ToString();
            TenantId = tenant.Id;
            Tenant = tenant;
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
            Brand = brand;
            CreatedAt = DateTimeOffset.UtcNow.LocalDateTime;
            UpdatedAt = DateTimeOffset.UtcNow.LocalDateTime;
        }

        public Product(string name, string? description, decimal price, bool isActive, string? brand)
        {
            Name = name;
            Description = description;
            Price = price;
            IsActive = isActive;
            Brand = brand;
            UpdatedAt = DateTimeOffset.UtcNow.LocalDateTime;
        }
    }
}
