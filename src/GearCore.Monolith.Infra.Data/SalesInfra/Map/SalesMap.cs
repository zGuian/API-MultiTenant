using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Map
{
    internal class SalesMap : IEntityTypeConfiguration<Sales>
    {
        public void Configure(EntityTypeBuilder<Sales> builder)
        {
            builder.ToTable("TB_SALES");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .HasColumnOrder(0)
                   .HasColumnName("COL_ID")
                   .IsRequired();

            builder.Property(s => s.TenantID)
                   .HasColumnOrder(1)
                   .HasColumnName("FK_TENANT_ID")
                   .IsRequired();

            builder.Property(s => s.UserId)
                   .HasColumnOrder(2)
                   .HasColumnName("FK_USER_ID")
                   .IsRequired();

            builder.Property(s => s.TotalAmount)
                   .HasColumnOrder(3)
                   .HasColumnName("COL_TOTAL_AMOUNT")
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(s => s.DiscountAmount)
                   .HasColumnOrder(4)
                   .HasColumnName("COL_DISCOUNT_AMOUNT")
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(s => s.FinalAmount)
                   .HasColumnOrder(5)
                   .HasColumnName("COL_FINAL_AMOUNT")
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(s => s.Status)
                   .HasColumnOrder(6)
                   .HasColumnName("COL_STATUS")
                   .HasConversion(s => s.ToString(),
                                  s => Enum.Parse<SaleStatus>(s))
                   .IsRequired();

            builder.HasOne(s => s.Tenant)
                .WithMany(t => t.Sales)
                .HasForeignKey(s => s.TenantID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
