using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.Commons.Structures;
using GearCore.Monolith.Core.SalesCore.Entities.Enums;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.SalesCore.Entities
{
    public class Sales : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public SaleStatus Status { get; set; }

        public virtual Tenant Tenant { get; set; } = default!;
        public override string TenantId { get; set; } = default!;

        public virtual User User { get; set; } = default!;
        public string UserId { get; set; } = default!;

        public virtual ICollection<SaleItem> SaleItems { get; set; } = [];

        public Sales()
        {
            Status = SaleStatus.Draft;
            SumTotalAmount();
            SumFinalAmount();
        }

        public Sales(ICollection<SaleItem> saleItems, string userId)
        {
            Status = SaleStatus.Draft;
            SaleItems = saleItems;
            UserId = userId;
            SumTotalAmount();
            SumFinalAmount();
        }

        public ValueResponse<bool> ConfirmSale()
        {
            if (Status == SaleStatus.Draft)
            {
                Status = SaleStatus.Confirmed;
                return ValueResponse<bool>.ReturnSuccess(true, "Pedido confirmado com sucesso!");
            }
            return ValueResponse<bool>.ReturnFalse("Status incorreto", false);
        }

        public void SumTotalAmount()
        {
            TotalAmount = SaleItems.Sum(i => i.TotalPrice);
        }

        public void SumFinalAmount()
        {
            FinalAmount = TotalAmount - DiscountAmount;
        }
    }
}
