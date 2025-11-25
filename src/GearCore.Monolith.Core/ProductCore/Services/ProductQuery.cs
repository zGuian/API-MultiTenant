using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;

namespace GearCore.Monolith.Core.ProductCore.Services
{
    public class ProductQuery(IProductQueryRepository productQuery) : IProductQuery
    {
        private readonly IProductQueryRepository _productQuery = productQuery;

        public async Task<ProductViewDto> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var product = await _productQuery.GetByIdAsync(id, ct);
            return new ProductViewDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
