using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;

namespace GearCore.Monolith.Infra.Data.StockInfra.Seed
{
    internal static class StockSeed
    {
        public static IEnumerable<Stock> GetSeeds(Product[] products)
        {
            var seeds = new List<Stock>();
            foreach (var item in products)
            {
                seeds.Add(new Stock(item.Tenant, item, Random.Shared.Next(10, 51), Random.Shared.Next(0, 11)));
            }
            return seeds;
        }
    }
}
