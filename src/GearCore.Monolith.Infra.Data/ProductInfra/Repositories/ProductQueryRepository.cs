using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Repositories
{
    public class ProductQueryRepository(AppDbContext context)
        : BaseQueryRepository<Product, Guid>(context)
        , IProductQueryRepository
    {

    }
}
