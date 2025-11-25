using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.StockCore.Entities;

namespace GearCore.Monolith.Core.StockCore.Interfaces.Repositories
{
    public interface IStockCommandRepository : IBaseCommandRepository<Stock, Guid>
    {
    }
}
