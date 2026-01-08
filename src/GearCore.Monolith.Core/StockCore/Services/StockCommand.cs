using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.DTOs;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.StockCore.Services
{
    public class StockCommand(IStockCommandRepository stockCommandRepository
        , IStockQueryRepository stockQueryRepository
        , IProductQueryRepository productQueryRepository
        , IUnitOfWork unitOfWork
        , ITenantProvider tenantProvider) : IStockCommand
    {
        private readonly IStockCommandRepository _stockCommandRepository = stockCommandRepository;
        private readonly IStockQueryRepository _stockQueryRepository = stockQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository = productQueryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public async Task AddStockAsync(RegisterStockDto registerDto, CancellationToken ct = default)
        {
            var stock = registerDto.Adapt<Stock>();
            stock.SetValuesToEntity(_tenantProvider.TenantId, registerDto.ProductId);
            await _stockCommandRepository.InsertAsync(stock, ct);
        }
    }
}
