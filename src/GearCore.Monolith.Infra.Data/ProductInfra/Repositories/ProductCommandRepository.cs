using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Repositories
{
    public class ProductCommandRepository(AppDbContext context
        , ILogger<ProductCommandRepository> logger
        , ITenantProvider tenantProvider)
        : BaseCommandRepository<Product, string>(context)
        , IProductCommandRepository
    {
        private readonly ILogger<ProductCommandRepository> _logger = logger;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly AppDbContext _context = context;

        public override async Task InsertAsync(Product entity, CancellationToken ct = default)
        {
            //HÁ UM PROCESO DE "round-trip" QUE PODE CAUSAR LENTIDÃO EM GRANDE ESCALA.
            entity.Tenant = await _context.Tenants.FindAsync([_tenantProvider.TenantId], ct) 
                ?? throw new NotFoundException("NÃO FOI ENCONTRADO TENANT INFORMADO!");
            await base.InsertAsync(entity, ct);
        }
    }
}
