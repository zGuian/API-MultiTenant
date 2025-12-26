using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;

namespace GearCore.Monolith.Core.TenantCore.Entities
{
    public class Tenant
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Subdomain { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TenantUser> TenantUsers { get; set; } = [];
        public virtual ICollection<Product> Products { get; set; } = [];
        public virtual ICollection<Sales> Sales { get; set; } = [];
        public virtual ICollection<Stock> Stocks { get; set; } = [];

        public Tenant()
        { }

        public Tenant(string id, string name, string? subdomain, bool isActive)
        {
            Id = id;
            Name = name;
            Subdomain = subdomain;
            IsActive = isActive;
        }
    }
}
