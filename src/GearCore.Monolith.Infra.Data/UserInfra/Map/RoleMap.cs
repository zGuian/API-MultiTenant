using GearCore.Monolith.Core.UserCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.UserInfra.Map
{
    internal class RoleMap : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("TB_ROLE");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasColumnOrder(0)
                .HasColumnName("COL_ID")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(r => r.RoleName)
                .HasColumnOrder(1)
                .HasColumnName("COL_NAME")
                .IsRequired();

            builder.Property(r => r.RoleDescription)
                .HasColumnOrder(2)
                .HasColumnName("COL_DESCRIPTION")
                .IsRequired();
        }
    }
}
