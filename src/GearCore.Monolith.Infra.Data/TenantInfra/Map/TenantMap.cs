using GearCore.Monolith.Core.TenantCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Map
{
    public class TenantMap : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("TB_TENANT");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("COL_ID")
                .HasColumnOrder(0)
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("COL_NAME")
                .HasColumnOrder(1)
                .IsRequired();

            builder.Property(t => t.Subdomain)
                .HasColumnOrder(2)
                .HasColumnName("COL_SUBDOMAIN")
                .IsRequired(false);

            builder.Property(t => t.IsActive)
                .HasColumnOrder(3)
                .HasColumnName("COL_IS_ACTIVE")
                .IsRequired();
        }
    }
}
