namespace GearCore.Monolith.Core.UserCore.Entities
{
    public class Roles
    {
        public string Id { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string RoleDescription { get; set; } = string.Empty;
        public virtual ICollection<UserRoles> UserRoles { get; set; } = [];
    }
}
