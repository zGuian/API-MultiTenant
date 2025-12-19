using GearCore.Monolith.Core.ProductCore.DTOs;

namespace GearCore.Monolith.Core.ProductCore.Interfaces.Services
{
    public interface IProductQuery
    {
        Task<IEnumerable<ProductViewDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken ct = default);
        Task<ProductViewDto> GetByIdAsync(string id, CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct = default);
    }
}
