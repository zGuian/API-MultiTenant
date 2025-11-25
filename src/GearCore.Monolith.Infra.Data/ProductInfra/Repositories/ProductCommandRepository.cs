using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Repositories
{
    public class ProductCommandRepository(AppDbContext context
        , ILogger<ProductCommandRepository> logger)
        : BaseCommandRepository<Product, Guid>(context)
        , IProductCommandRepository
    {
        private readonly ILogger<ProductCommandRepository> _logger = logger;
    }
}
