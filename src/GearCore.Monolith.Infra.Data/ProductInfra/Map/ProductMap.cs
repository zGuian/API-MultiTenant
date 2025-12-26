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

            builder.HasIndex(p => new { p.TenantId, p.Name, p.Brand })
                .IsUnique();

            builder.Property(p => p.TenantId)
                .HasColumnName("FK_TENANT_ID")
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasColumnName("COL_NAME")
                .HasColumnOrder(1)
                .HasMaxLength(150)
                .HasConversion(v => v.ToUpper(),
                               v => v)
                .IsRequired(true);

            builder.Property(p => p.Description)
                .HasColumnName("COL_DESCRIPTION")
                .HasColumnOrder(2)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.Price)
                .HasPrecision(18, 2)
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
                .IsRequired(false);

            builder.HasOne(p => p.Tenant)
                .WithMany(t => t.Products)
                .HasForeignKey(p => p.TenantId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
