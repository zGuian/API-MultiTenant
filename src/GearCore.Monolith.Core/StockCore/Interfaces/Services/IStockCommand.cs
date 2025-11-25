using GearCore.Monolith.Core.StockCore.DTOs;

namespace GearCore.Monolith.Core.StockCore.Interfaces.Services
{
    public interface IStockCommand
    {
        Task AddStockAsync(RegisterStockDto registerDto, CancellationToken ct = default);
    }
}
