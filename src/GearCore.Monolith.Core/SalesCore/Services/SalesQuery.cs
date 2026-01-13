using GearCore.Monolith.Core.SalesCore.DTOs;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;
using Mapster;

namespace GearCore.Monolith.Core.SalesCore.Services
{
    public class SalesQuery(ISalesQueryRepository salesQueryRepository) : ISalesQuery
    {
        private readonly ISalesQueryRepository _salesQueryRepository = salesQueryRepository;

        public async Task<int> CountAsync(CancellationToken ct = default) => await _salesQueryRepository.CountAsync(ct);

        public async Task<IEnumerable<SaleViewDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken ct = default)
        {
            var sales = await _salesQueryRepository.GetPagedAsync(pageIndex, pageSize, ct);
            return sales.Adapt<IEnumerable<SaleViewDto>>();
        }
    }
}
