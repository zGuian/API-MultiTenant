using GearCore.Monolith.Core.ProductCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Map
{
    internal class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("TB_PRODUCT");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasColumnName("COL_ID")
                .HasColumnOrder(0)
                .IsRequired(true);

            builder.Property(p => p.Name)
                .HasColumnName("COL_NAME")
                .HasColumnOrder(1)
                .HasMaxLength(150)
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasColumnName("COL_DESCRIPTION")
                .HasColumnOrder(2)
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(p => p.Price)
                .HasPrecision(18,2)
                .HasColumnName("COL_PRICE")
                .HasColumnOrder(3)
                .IsRequired(true);

            builder.Property(p => p.IsActive)
                .HasColumnName("COL_IS_ACTIVE")
                .HasColumnOrder(4)
                .IsRequired(true);

            builder.Property(p => p.Brand)
                .HasColumnName("COL_BRAND")
                .HasColumnOrder(5)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(p => p.CreatedAt)
               .HasColumnName("COL_CREATED_AT")
               .HasColumnOrder(6)
               .IsRequired(true);

            builder.Property(p => p.UpdateAt)
                .HasColumnName("COL_UPDATED_AT")
                .HasColumnOrder(7)
                .IsRequired(true);
        }
    }
}
