using GearCore.Monolith.Core.StockCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.StockInfra.Map
{
    //internal class StockMovimentsMap : IEntityTypeConfiguration<StockMoviments>
    //{
    //    public void Configure(EntityTypeBuilder<StockMoviments> builder)
    //    {
    //        builder.ToTable("TB_STOCK_MOVIMENTS");

    //        builder.HasKey(s => s.Id);
    //        builder.Property(sm => sm.Id)
    //            .ValueGeneratedNever()
    //            .HasColumnName("COL_ID")
    //            .HasColumnOrder(0)
    //            .IsRequired();

    //        builder.Property(s => s.ProductId)
    //            .HasColumnName("COL_FK_PRODUCTID")
    //            .HasColumnOrder(1)
    //            .IsRequired();

    //        builder.Property(s => s.Quantity)
    //            .HasColumnName("COL_QUANTITY")
    //            .HasColumnOrder(2)
    //            .IsRequired();

    //        builder.Property(s => s.PreviousQuantity)
    //            .HasColumnName("COL_PREVIOUS_QUANTITY")
    //            .HasColumnOrder(3)
    //            .IsRequired();

    //        builder.Property(s => s.NewQuantity)
    //            .HasColumnName("COL_NEW_QUANTITY")
    //            .HasColumnOrder(4)
    //            .IsRequired();

    //        builder.Property(s => s.MovementDate)
    //            .HasColumnName("COL_MOVEMENT_DATE")
    //            .HasColumnOrder(5)
    //            .IsRequired();

    //        builder.Property(s => s.MovementType)
    //            .HasColumnName("COL_MOVEMENT_TYPE")
    //            .HasColumnOrder(6)
    //            .IsRequired();

    //        builder.Property(s => s.Origin)
    //            .HasColumnName("COL_ORIGIN")
    //            .HasColumnOrder(7)
    //            .IsRequired();

    //        builder.Property(s => s.Observation)
    //            .HasColumnName("COL_OBSERVATION")
    //            .HasColumnOrder(8)
    //            .IsRequired(false);

    //        builder.HasOne(m => m.Product)
    //            .WithMany(p => p.Moviments)
    //            .HasForeignKey(m => m.ProductId)
    //            .OnDelete(DeleteBehavior.Restrict);
    //    }
    //}
}
