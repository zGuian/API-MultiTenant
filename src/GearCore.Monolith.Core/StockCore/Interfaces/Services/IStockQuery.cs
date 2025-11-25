using GearCore.Monolith.Core.StockCore.DTOs;

namespace GearCore.Monolith.Core.StockCore.Interfaces.Services
{
    public interface IStockQuery
    {
        Task<HashSet<StockViewDto>> GetAllStocksAsync(CancellationToken ct = default);
        Task<StockViewDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
