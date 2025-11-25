using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.StockCore.Services
{
    public class StockQuery(IStockQueryRepository stockQueryRepository) : IStockQuery
    {
        private readonly IStockQueryRepository _stockQueryRepository = stockQueryRepository;

        public async Task<HashSet<StockViewDto>> GetAllStocksAsync(CancellationToken ct = default)
        {
            var stocks = await _stockQueryRepository.GetAllAsync(ct);
            return stocks.Adapt<HashSet<StockViewDto>>();
        }

        public async Task<StockViewDto> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var stock = await _stockQueryRepository.GetByIdAsync(id, ct);
            return stock == null
                ? throw new KeyNotFoundException($"Stock with Id {id} was not found.")
                : stock.Adapt<StockViewDto>();
        }
    }
}
