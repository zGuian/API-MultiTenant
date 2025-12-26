using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.Data.UserInfra.Seed
{
    internal static class UserRoleSeed
    {
        public static IEnumerable<UserRoles> GetSeeds(User[] users, Roles[] roles)
        {
            int prize;
            var seeds = new List<UserRoles>();
            for (int i = 0; i < users.Length; i++)
            {
                var user = users[i];
                prize = Random.Shared.Next(0, roles.Length);
                var role = roles[prize];
                seeds.Add(new UserRoles(user, role));
            }
            return seeds;
        }
    }
}
