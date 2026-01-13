using GearCore.Monolith.Core.UserCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GearCore.Monolith.Infra.Data.UserInfra.Map
{
    internal class UserRoleMap : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable("TB_USER_ROLE");

            builder.HasKey(ur => new { ur.UserID, ur.RoleID });

            builder.Property(ur => ur.UserID)
                .HasColumnName("FK_USER_ID")
                .IsRequired();

            builder.Property(ur => ur.RoleID)
                .HasColumnName("FK_ROLE_ID")
                .IsRequired();

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Roles)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(r => r.RoleID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
