using GearCore.Monolith.Core.SalesCore.DTOs;

namespace GearCore.Monolith.Core.SalesCore.Interfaces.Services
{
    public interface ISalesQuery
    {
        Task<int> CountAsync(CancellationToken ct = default);
        Task<IEnumerable<SaleViewDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken ct = default);
    }
}
