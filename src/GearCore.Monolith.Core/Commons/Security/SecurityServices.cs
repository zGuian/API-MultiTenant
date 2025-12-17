using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Core.Commons.Security
{
    public static class SecurityServices
    {
        public static string ConvertPasswordInHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
