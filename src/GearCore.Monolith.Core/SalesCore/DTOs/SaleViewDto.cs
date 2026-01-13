using GearCore.Monolith.Core.SalesCore.Entities.Enums;

namespace GearCore.Monolith.Core.SalesCore.DTOs
{
    public class SaleViewDto
    {
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public SaleStatus Status { get; set; }
        public string TenantId { get; set; } = default!;
        public virtual ICollection<SaleItemViewDto> SaleItems { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
