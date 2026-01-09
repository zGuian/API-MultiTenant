using Dapper;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Core.ProductCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GearCore.Monolith.Infra.Data.ProductInfra.Repositories
{
    public class ProductQueryRepository(AppDbContext context
        , ITenantProvider tenantProvider
        , IConfiguration configuration)
        : BaseQueryRepository<Product, string>(context, tenantProvider)
        , IProductQueryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            var count = await _context.Products.Where(p => p.Tenant.Id == _tenantProvider.TenantId)
                                               .CountAsync(ct);
            return count;
        }

        public async Task<IEnumerable<Product>> GetManyProductById(IEnumerable<string> ids)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();

                var sql = $@"SELECT COL_ID AS Id
                             , COL_NAME AS Name
                             , COL_DESCRIPTION AS Description
                             , COL_PRICE AS Price
                             , COL_IS_ACTIVE AS IsActive
                             , COL_BRAND AS Brand
                             , COL_CREATE_AT AS CreatedAt
                             , COL_UPDATE_AT AS UpdatedAt
                             FROM TB_PRODUCT
                             WHERE COL_ID IN @Ids";

                var parameters = new
                {
                    Ids = ids
                };

                var row = await conn.QueryAsync<Product>(sql, parameters, commandTimeout: 30, commandType: CommandType.Text);
                if (row != null)
                {
                    return row;
                }
                throw new NotFoundException("Não foi encontrado nenhum produto para os IDs fornecidos");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct = default)
        {
            var products = await _context.Products.AsNoTracking()
                                                  .Where(p => p.Tenant.Id == _tenantProvider.TenantId)
                                                  .OrderBy(p => p.CreatedAt)
                                                  .Skip((pageIndex - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync(ct);
            return products;
        }
    }
}
