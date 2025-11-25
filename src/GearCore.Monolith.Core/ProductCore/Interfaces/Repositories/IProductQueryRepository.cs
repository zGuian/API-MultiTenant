using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.ProductCore.Entities;

namespace GearCore.Monolith.Core.ProductCore.Interfaces.Repositories
{
    public interface IProductQueryRepository : IBaseQueryRepository<Product, Guid>
    {
    }
}
