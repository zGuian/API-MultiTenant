using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using Mapster;
using MapsterMapper;

namespace GearCore.Monolith.Core.StockCore.Services
{
    public class StockQuery(IStockQueryRepository stockQueryRepository
        , IMapper mapper) : IStockQuery
    {
        private readonly IStockQueryRepository _stockQueryRepository = stockQueryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<HashSet<StockViewDto>> GetAllStocksAsync(CancellationToken ct = default)
        {
            var stocks = await _stockQueryRepository.GetAllAsync(ct);
            return _mapper.Map<HashSet<StockViewDto>>(stocks);
        }

        public async Task<StockViewDto> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var stock = await _stockQueryRepository.GetByIdAsync(id, ct);
            return stock == null
                ? throw new KeyNotFoundException($"Stock with Id {id} was not found.")
                : stock.Adapt<StockViewDto>();
        }
    }
}
