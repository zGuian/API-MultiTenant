using Dapper;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Core.StockCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GearCore.Monolith.Infra.Data.StockInfra.Repositories
{
    public class StockQueryRepository(AppDbContext context
        , ILogger<StockQueryRepository> logger
        , ITenantProvider tenantProvider
        , IConfiguration configuration)
        : BaseQueryRepository<Stock, string>(context, tenantProvider), IStockQueryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<StockQueryRepository> _logger = logger;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");

        public override async Task<HashSet<Stock>> GetAllAsync(CancellationToken ct = default)
        {
            var result = await _context.Stocks.Where(s => s.TenantId == _tenantProvider.TenantId)
                                              .Select(s => new Stock
                                              {
                                                  Id = s.Id,
                                                  TenantId = s.Tenant.Id,
                                                  Product = new Product
                                                  {
                                                      Id = s.Product.Id,
                                                      Name = s.Product.Name,
                                                      Description = s.Product.Description,
                                                      Price = s.Product.Price,
                                                      CreatedAt = s.Product.CreatedAt
                                                  },
                                                  Quantity = s.Quantity,
                                                  ReservedQuantity = s.ReservedQuantity,
                                                  Active = s.Active,
                                                  UpdatedAt = s.UpdatedAt
                                              })
                                             .ToHashSetAsync(ct);
            return result;
        }

        public override async Task<Stock> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var result = await _context.Stocks.Where(s => s.TenantId == _tenantProvider.TenantId)
                                              .Include(s => s.Product)
                                              .SingleAsync(s => s.Id == id, ct);
            return result;
        }

        public async Task<IEnumerable<Stock>> GetManyProductInStock(IEnumerable<string> ids, CancellationToken ct = default)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync(ct);

                var sql = $@"SELECT 
                                 stock.[COL_ID] AS Id
                                ,stock.[COL_QUANTITY] AS Quantity
                                ,stock.[COL_RESERVED_QUANTITY] AS ReservedQuantity
                                ,product.[COL_ID] AS Id
                                ,product.[COL_NAME] AS Name
                                ,product.[COL_DESCRIPTION] AS Description
                                ,product.[COL_PRICE] AS Price
                                ,product.[COL_IS_ACTIVE] AS IsActive
                                ,product.[COL_BRAND] AS Brand
                             FROM [dbo].[TB_STOCK] AS stock
                             LEFT JOIN TB_PRODUCT AS product
                             ON stock.FK_PRODUCT_ID = product.COL_ID
                             WHERE product.COL_ID IN @Ids";

                var parameters = new
                {
                    Ids = ids
                };

                var result = await conn.QueryAsync<Stock, Product, Stock>(sql, (stock, product) =>
                {
                    stock.Product = product;
                    return stock;
                },
                param: parameters, commandTimeout: 30, commandType: CommandType.Text);

                return result ?? throw new NotFoundException("Não foi encontrado nenhum produto para os IDs fornecidos");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
