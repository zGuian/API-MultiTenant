using GearCore.Monolith.Core.ProductCore.DTOs;

namespace GearCore.Monolith.Core.ProductCore.Interfaces.Services
{
    public interface IProductCommand
    {
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<ProductViewDto> RegisterAsync(ProductRegisterDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(ProductUpdateDto dto, CancellationToken cancellationToken = default);
    }
}
