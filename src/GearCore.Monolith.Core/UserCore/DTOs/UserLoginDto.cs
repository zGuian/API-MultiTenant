using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GearCore.Monolith.Core.UserCore.DTOs
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
