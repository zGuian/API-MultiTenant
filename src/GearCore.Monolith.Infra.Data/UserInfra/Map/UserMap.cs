using GearCore.Monolith.Core.UserCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.UserInfra.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USERS");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("COL_ID")
                .HasColumnOrder(0);

            builder.Property(u => u.FirstName)
                .HasColumnName("COL_FIRST_NAME")
                .HasColumnOrder(1)
                .HasMaxLength(60);

            builder.HasIndex(u => u.NormalizedFirstName);
            builder.Property(u => u.NormalizedFirstName)
                .HasColumnName("COL_NORMALIZED_FIRSTNAME")
                .HasColumnOrder(2)
                .HasMaxLength(60);

            builder.Property(u => u.LastName)
                .HasColumnName("COL_LAST_NAME")
                .HasColumnOrder(3)
                .HasMaxLength(60);

            builder.HasIndex(u => u.NormalizedLastName);
            builder.Property(u => u.NormalizedLastName)
                .HasColumnName("COL_NORMALIZED_LASTNAME")
                .HasColumnOrder(4)
                .HasMaxLength(60);

            builder.HasIndex(u => u.CompleteName);
            builder.Property(u => u.CompleteName)
                .HasColumnName("COL_COMPLETE_NAME")
                .HasColumnOrder(5)
                .HasMaxLength(120)
                .IsRequired(true);

            builder.HasAlternateKey(u => u.Email);
            builder.Property(u => u.Email)
                .HasColumnName("COL_EMAIL")
                .HasColumnOrder(6)
                .HasMaxLength(60);

            builder.HasIndex(u => u.NormalizedEmail)
                .IsUnique();
            builder.Property(u => u.NormalizedEmail)
                .HasColumnName("COL_NORMALIZED_EMAIL")
                .HasColumnOrder(7)
                .HasMaxLength(60);

            builder.Property(u => u.EmailConfirmed)
                .HasColumnOrder(8)
                .HasColumnName("COL_EMAIL_CONFIRMED");

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("COL_PHONE_NUMBER")
                .HasColumnOrder(9)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(u => u.PhoneNumberConfirmed)
                .HasColumnName("COL_PHONE_NUMBER_CONFIRMED")
                .HasColumnOrder(10);

            builder.Property(u => u.AccessFailedCount)
                .HasColumnName("COL_ACCESS_FAILED_COUNT")
                .HasColumnOrder(11);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("COL_PASSWORD_HASH")
                .HasColumnOrder(12)
                .HasMaxLength(255);
        }
    }
}
