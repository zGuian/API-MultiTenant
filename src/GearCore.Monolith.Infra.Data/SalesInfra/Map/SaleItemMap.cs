using GearCore.Monolith.Core.SalesCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Map
{
    internal class SaleItemMap : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("TB_SALE_ITEM");

            builder.HasKey(si => si.Id);
            builder.Property(si => si.Id)
                   .HasColumnOrder(0)
                   .HasColumnName("COL_ID")
                   .IsRequired();

            builder.Property(si => si.SaleId)
                   .HasColumnOrder(1)
                   .HasColumnName("FK_SALE_ID")
                   .IsRequired();

            builder.Property(si => si.ProductId)
                   .HasColumnOrder(2)
                   .HasColumnName("FK_PRODUCT_ID")
                   .IsRequired();

            builder.Property(si => si.ProductName)
                   .HasColumnOrder(3)
                   .HasColumnName("COL_PRODUCT_NAME")
                   .IsRequired();

            builder.Property(si => si.UnitPrice)
                   .HasColumnOrder(4)
                   .HasColumnName("COL_UNIT_PRICE")
                   .HasPrecision(18,2)
                   .IsRequired();

            builder.Property(si => si.Quantity)
                   .HasColumnOrder(5)
                   .HasColumnName("COL_QUANTITY")
                   .IsRequired();

            builder.Property(si => si.TotalPrice)
                   .HasColumnOrder(6)
                   .HasColumnName("COL_TOTAL_PRICE")
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.HasOne(si => si.Sale)
                   .WithMany(s => s.SaleItems)
                   .HasForeignKey(si => si.SaleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(si => si.Product)
                   .WithMany(s => s.SaleItems)
                   .HasForeignKey(si => si.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
