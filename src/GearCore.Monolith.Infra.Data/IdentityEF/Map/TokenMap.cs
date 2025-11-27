using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Map
{
    internal class TokenMap : IEntityTypeConfiguration<ApplicationToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationToken> builder)
        {
            builder.ToTable("TB_USER_TOKEN");

            builder.Property(t => t.UserId)
                .HasColumnName("COL_USER_ID")
                .HasColumnOrder(0)
                .IsRequired();

            builder.Property(t => t.LoginProvider)
                .HasColumnName("COL_LOGIN_PROVIDER")
                .HasColumnOrder(1)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("COL_NAME")
                .HasColumnOrder(2)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Value)
                .HasColumnName("COL_VALUE")
                .HasColumnOrder(3);
        }
    }
}
