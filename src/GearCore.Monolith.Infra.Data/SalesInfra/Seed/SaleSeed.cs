using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Seed
{
    internal static class SaleSeed
    {
        public static IEnumerable<Sales> GetSeeds(Tenant[] tenants, User[] users)
        {
            var seeds = new List<Sales>();
            return seeds;
        }
    }
}
