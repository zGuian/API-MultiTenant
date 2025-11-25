using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.StockCore.Services
{
    public class StockCommand(IStockCommandRepository stockCommandRepository
        , IStockQueryRepository stockQueryRepository
        , IUnitOfWork unitOfWork) : IStockCommand
    {
        private readonly IStockCommandRepository _stockCommandRepository = stockCommandRepository;
        private readonly IStockQueryRepository _stockQueryRepository = stockQueryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task AddStockAsync(RegisterStockDto registerDto, CancellationToken ct = default)
        {
            var stock = registerDto.Adapt<Stock>();
            await _stockCommandRepository.InsertAsync(stock, ct);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
