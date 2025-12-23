using GearCore.Monolith.Core.Commons.Entities;
using GearCore.Monolith.Core.Commons.Security;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Entities;

namespace GearCore.Monolith.Core.UserCore.Entities
{
    public class User : BaseEntity, IUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string NormalizedFirstName { get; private set; }
        public string LastName { get; set; } = string.Empty;
        public string NormalizedLastName { get; private set; }
        public string CompleteName { get; private set; }
        public string Email { get; set; } = string.Empty;
        public string NormalizedEmail { get; private set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; } = false;
        public int AccessFailedCount { get; set; }
        public string PasswordHash { get; set; } = string.Empty;

        public virtual ICollection<UserRoles> UserRoles { get; set; } = [];
        public virtual ICollection<TenantUser> TenantUsers { get; set; } = [];
        public virtual ICollection<Sales> Sales { get; set; } = [];

        public User()
        {
            NormalizedFirstName = FirstName.ToUpper();
            NormalizedLastName = LastName.ToUpper();
            NormalizedEmail = Email.ToUpper();
            CompleteName = $"{NormalizedFirstName} {NormalizedLastName}";
        }

        public User(string firtsName, string lastName, string email, string password, string? phoneNumber)
        {
            Id = Guid.NewGuid().ToString();
            FirstName = firtsName;
            LastName = lastName;
            Email = email;
            NormalizedFirstName = firtsName.ToUpper();
            NormalizedLastName = lastName.ToUpper();
            NormalizedEmail = Email.ToUpper();
            CompleteName = $"{NormalizedFirstName} {NormalizedLastName}";
            PasswordHash = SecurityServices.ConvertPasswordInHash(password);
            PhoneNumber = phoneNumber;
            CreatedAt = DateTimeOffset.UtcNow.LocalDateTime;
            UpdatedAt = DateTimeOffset.UtcNow.LocalDateTime;
        }
    }
}
