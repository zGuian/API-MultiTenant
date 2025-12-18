using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.ProductCore.Services
{
    public class ProductQuery(IProductQueryRepository productQuery) : IProductQuery
    {
        private readonly IProductQueryRepository _productQuery = productQuery;

        public async Task<ProductViewDto> GetByIdAsync(string id, CancellationToken ct)
        {
            var product = await _productQuery.GetByIdAsync(id, ct);
            return product.Adapt<ProductViewDto>();
        }
    }
}
