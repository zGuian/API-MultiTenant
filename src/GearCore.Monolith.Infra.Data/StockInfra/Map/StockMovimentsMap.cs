using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.StockInfra.Map
{
    internal class StockMovimentsMap : IEntityTypeConfiguration<StockMoviments>
    {
        public void Configure(EntityTypeBuilder<StockMoviments> builder)
        {
            builder.ToTable("TB_STOCK_MOVIMENTS");

            builder.HasKey(s => s.Id);
            builder.Property(sm => sm.Id)
                   .ValueGeneratedNever()
                   .HasColumnName("COL_ID")
                   .HasColumnOrder(0)
                   .IsRequired();

            builder.Property(sm => sm.StocksId)
                   .HasColumnOrder(1)
                   .HasColumnName("FK_STOCK_ID")
                   .IsRequired();

            builder.Property(s => s.Type)
                   .HasColumnName("COL_TYPE")
                   .HasColumnOrder(2)
                   .HasConversion(sm => sm.ToString(), 
                               sm => Enum.Parse<StockMovementType>(sm))
                   .IsRequired();

            builder.Property(s => s.Quantity)
                   .HasColumnName("COL_QUANTITY")
                   .HasColumnOrder(3)
                   .IsRequired();

            builder.Property(s => s.Reason)
                   .HasColumnName("COL_REASON")
                   .HasColumnOrder(4)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.ReferenceId)
                   .HasColumnName("COL_REFERENCE_ID")
                   .HasColumnOrder(5)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(s => s.CreatedAt)
                   .HasColumnName("COL_CREATE_AT")
                   .HasColumnOrder(6)
                   .IsRequired();

            builder.HasOne(sm => sm.Stocks)
                   .WithMany(s => s.StockMoviments)
                   .HasForeignKey(sm => sm.StocksId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
