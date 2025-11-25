using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Map
{
    internal class RoleMap : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.ToTable("TB_ROLE");

            builder.Property(r => r.Id)
                .HasColumnName("COL_ID")
                .HasColumnOrder(0);

            builder.Property(r => r.Name)
                .HasColumnName("COL_NAME")
                .HasColumnOrder(2)
                .HasMaxLength(60);

            builder.Property(r => r.NormalizedName)
                .HasColumnName("COL_NORMALIZED_NAME")
                .HasColumnOrder(3)
                .HasMaxLength(60);

            builder.Property(r => r.ConcurrencyStamp)
                .HasColumnName("COL_CONCURRENCY_STAMP")
                .HasMaxLength(255);

            throw new NotImplementedException();
        }
    }
}
