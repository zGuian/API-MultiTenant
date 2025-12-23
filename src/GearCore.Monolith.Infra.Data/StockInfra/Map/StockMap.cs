using GearCore.Monolith.Core.StockCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.StockInfra.Map
{
    internal class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("TB_STOCK");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                   .ValueGeneratedNever()
                   .HasColumnOrder(0)
                   .HasColumnName("COL_ID")
                   .IsRequired();

            builder.Property(s => s.TenantId)
                   .HasColumnName("FK_TENANT_ID")
                   .HasColumnOrder(1)
                   .IsRequired();

            builder.Property(s => s.ProductId)
                   .HasColumnName("FK_PRODUCT_ID")
                   .HasColumnOrder(2)
                   .IsRequired();

            builder.Property(s => s.StockMovimentsId)
                   .HasColumnName("FK_STOCK_MOVIMENTS")
                   .HasColumnOrder(3)
                   .IsRequired();

            builder.Property(s => s.Quantity)
                   .HasColumnOrder(4)
                   .HasColumnName("COL_QUANTITY")
                   .HasPrecision(18, 3)
                   .IsRequired();

            builder.Property(s => s.ReservedQuantity)
                   .HasColumnOrder(5)
                   .HasColumnName("COL_RESERVED_QUANTITY")
                   .HasPrecision(18, 3)
                   .IsRequired();

            builder.Property(s => s.Active)
                   .HasColumnOrder(6)
                   .HasColumnName("COL_ACTIVE")
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(s => s.StockMoviments)
                   .WithMany(sm => sm.Stocks)
                   .HasForeignKey(s => s.StockMovimentsId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
