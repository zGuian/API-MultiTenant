using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.ProductCore.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearCore.Monolith.Core.StockCore.Entities
{
    public class Stock : BaseEntity
    {
        [NotMapped]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        public int CurrentQuantity { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = new();

        public Stock() { }
    }
}
