using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.IdentityEF.Map
{
    internal class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("TB_USER");

            builder.Property(u => u.Id)
                .HasColumnName("COL_ID")
                .HasColumnOrder(0);

            builder.Property(u => u.UserName)
                .HasColumnName("COL_USERNAME")
                .HasColumnOrder(2)
                .HasMaxLength(60);

            builder.Property(u => u.NormalizedUserName)
                .HasColumnName("COL_NORMALIZED_USERNAME")
                .HasColumnOrder(3)
                .HasMaxLength(60);

            builder.Property(u => u.Email)
                .HasColumnName("COL_EMAIL")
                .HasColumnOrder(4)
                .HasMaxLength(60);

            builder.Property(u => u.NormalizedEmail)
                .HasColumnName("COL_NORMALIZED_EMAIL")
                .HasColumnOrder(5)
                .HasMaxLength(60);

            builder.Property(u => u.EmailConfirmed)
                .HasColumnName("COL_EMAIL_CONFIRMED");

            builder.Property(u => u.PasswordHash)
                .HasColumnName("COL_PASSWORD_HASH")
                .HasMaxLength(255);

            builder.Property(u => u.SecurityStamp)
                .HasColumnName("COL_SECURITY_STAMP")
                .HasMaxLength(255);

            builder.Property(u => u.ConcurrencyStamp)
                .HasColumnName("COL_CONCURRENCY_STAMP")
                .HasMaxLength(255);

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("COL_PHONE_NUMBER")
                .HasMaxLength(20);

            builder.Property(u => u.PhoneNumberConfirmed)
                .HasColumnName("COL_PHONE_NUMBER_CONFIRMED");

            builder.Property(u => u.TwoFactorEnabled)
                .HasColumnName("COL_TWO_FACTOR_ENABLED");

            builder.Property(u => u.LockoutEnd)
                .HasColumnName("COL_LOCKOUT_END");

            builder.Property(u => u.LockoutEnabled)
                .HasColumnName("COL_LOCKOUT_ENABLED");

            builder.Property(u => u.AccessFailedCount)
                .HasColumnName("COL_ACCESS_FAILED_COUNT");
        }
    }
}
