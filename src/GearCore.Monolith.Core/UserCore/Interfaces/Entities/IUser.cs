using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Core.UserCore.Interfaces.Entities
{
    public interface IUser
    {
        string Id { get; }
        string FirstName { get; set; }
        string NormalizedFirstName { get; }
        string LastName { get; set; }
        string NormalizedLastName { get; }
        string Email { get; set; }
        string NormalizedEmail { get; }
        string PasswordHash { get; set; }
        bool EmailConfirmed { get; set; }
        string? PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        int AccessFailedCount { get; set; }
        ICollection<TenantUser> TenantUsers { get; set; }
    }
}