using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.ProductCore.DTOs;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Core.ProductCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.ProductCore.Services
{
    public class ProductCommand(IProductCommandRepository productCommand
        , IProductQueryRepository productQuery
        , IUnitOfWork unitOfWork) : IProductCommand
    {
        private readonly IProductCommandRepository _productCommand = productCommand;
        private readonly IProductQueryRepository _productQuery = productQuery;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<ProductViewDto> RegisterAsync(ProductRegisterDto dto, CancellationToken ct = default)
        {
            var product = dto.Adapt<Product>();
            await _productCommand.InsertAsync(product, ct);
            await _unitOfWork.CommitAsync(ct);
            return product.Adapt<ProductViewDto>();
        }

        public async Task UpdateAsync(ProductUpdateDto dto, CancellationToken ct = default)
        {
            var product = await _productQuery.GetByIdAsync(dto.Id, ct)
                ?? throw new KeyNotFoundException($"Product with Id {dto.Id} not found.");
            dto.Adapt(product);
            _productCommand.Update(product);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _productCommand.Delete(id);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
