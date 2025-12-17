namespace GearCore.Monolith.Core.UserCore.Entities
{
    public class UserRoles
    {
        public string UserID { get; set; } = default!;
        public User User { get; set; } = default!;

        public string RoleID { get; set; } = default!;
        public Roles Roles { get; set; } = default!;
    }
}
