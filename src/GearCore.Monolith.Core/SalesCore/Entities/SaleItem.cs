using GearCore.Monolith.Core.ProductCore.Entities;

namespace GearCore.Monolith.Core.SalesCore.Entities
{
    public class SaleItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public string SaleId { get; set; } = default!;
        public virtual Sales Sale { get; set; } = new();

        public string ProductId { get; set; } = default!;
        public virtual Product Product { get; set; } = new();

        public SaleItem()
        { }

        public SaleItem(Product product, int quantity)
        {
            Product = product;
            ProductName = product.Name;
            UnitPrice = product.Price;
            Quantity = quantity;
            SumTotalPrice();
        }

        public void SumTotalPrice()
        {
            TotalPrice = UnitPrice * Quantity;
        }
    }
}
