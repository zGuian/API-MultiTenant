using GearCore.Monolith.Core.ProductCore.DTOs;

namespace GearCore.Monolith.Core.ProductCore.Interfaces.Services
{
    public interface IProductQuery
    {
        Task<ProductViewDto> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
