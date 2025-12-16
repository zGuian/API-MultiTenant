namespace GearCore.Monolith.Core.UserCore.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string NormalizedUserName { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
    }
}
