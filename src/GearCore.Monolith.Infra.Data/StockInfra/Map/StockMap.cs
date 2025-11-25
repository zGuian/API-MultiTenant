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

            builder.HasKey(s => s.ProductId);
            builder.HasOne(s => s.Product)
                .WithOne(p => p.Stock)
                .HasForeignKey<Stock>(s => s.ProductId);

            builder.Property(s => s.ProductId)
                .HasColumnName("COL_FK_PRODUCTID")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(s => s.CurrentQuantity)
                .HasColumnName("COL_CURRENT_QUANTITY")
                .HasColumnOrder(1)
                .IsRequired(true);
        }
    }
}
