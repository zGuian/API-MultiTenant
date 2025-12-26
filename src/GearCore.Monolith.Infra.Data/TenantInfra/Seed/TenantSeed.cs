using GearCore.Monolith.Core.TenantCore.Entities;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Seed
{
    internal static class TenantSeed
    {
        public static IEnumerable<Tenant> GetSeeds()
        {
            var tenants = new List<Tenant>
            {
                new("24eed906-45be-4d57-a056-c7c480b9c915", "TenantSeed1", "SubTenantSeed1", true),
                new("4213458c-9554-4de5-998e-ca06ed257749", "TenantSeed2", "Alimentos", true),
                new("d5fffc28-f956-4c85-92bf-b6d8c961bb11", "TenantSeed3", null, true)
            };

            return tenants;
        }
    }
}
