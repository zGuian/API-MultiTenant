using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Seed
{
    internal static class TenantUserSeed
    {
        public static IEnumerable<TenantUser> GetSeeds(Tenant[] tenants, User[] users)
        {
            int prize;
            var seeds = new List<TenantUser>();
            for (int i = 0; i < users.Length; i++)
            {
                var user = users[i];
                prize = Random.Shared.Next(0, tenants.Length);
                var tenant = tenants[prize];


                seeds.Add(new TenantUser(user, tenant));
            }

            return seeds;
        }
    }
}
