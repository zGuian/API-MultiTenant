using GearCore.Monolith.Core.TenantCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Map
{
    public class TenantUserMap : IEntityTypeConfiguration<TenantUser>
    {
        public void Configure(EntityTypeBuilder<TenantUser> builder)
        {
            builder.ToTable("TB_TENANT_USER");

            // Chave composta
            builder.HasKey(tu => new { tu.TenantId, tu.UserId });

            builder.Property(tu => tu.TenantId)
                .HasColumnName("FK_TENANT_ID")
                .IsRequired();

            builder.Property(tu => tu.UserId)
                .HasColumnName("FK_USER_ID")
                .IsRequired();

            builder.Property(tu => tu.Role)
                .HasColumnName("COL_ROLE")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(tu => tu.IsActive)
                .HasColumnName("COL_IS_ACTIVE")
                .IsRequired();

            builder.Property(tu => tu.CreatedBy)
                .HasColumnName("COL_CREATED_BY")
                .IsRequired();

            builder.HasOne(tu => tu.Tenant)
                .WithMany(t => t.TenantUsers)
                .HasForeignKey(tu => tu.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tu => tu.User)
                .WithMany(u => u.TenantUsers)
                .HasForeignKey(tu => tu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
