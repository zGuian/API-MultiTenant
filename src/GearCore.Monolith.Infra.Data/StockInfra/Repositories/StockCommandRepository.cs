using Dapper;
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
    public class StockCommandRepository(AppDbContext context
        , ILogger<StockCommandRepository> logger
        , IConfiguration configuration) : BaseCommandRepository<Stock, string>(context)
        , IStockCommandRepository
    {
        private readonly ILogger<StockCommandRepository> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");

        public override async Task InsertAsync(Stock entity, CancellationToken ct = default)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync(ct);
                var sql = $@"INSERT INTO TB_STOCK 
                             VALUES(@col_id, @fk_tenant_id, @fk_product_id, @quantity, @reserved_value, @isActive , @createAt, @updateAt)";

                var parameters = new
                {
                    col_id = Guid.NewGuid().ToString(),
                    fk_tenant_id = entity.TenantId,
                    fk_product_id = entity.ProductId,
                    quantity = entity.Quantity,
                    reserved_value = entity.ReservedQuantity,
                    isActive = entity.Active,
                    createAt = entity.CreatedAt,
                    updateAt = entity.UpdatedAt,
                };

                var begin = await conn.BeginTransactionAsync(ct);
                var rowCount = await conn.ExecuteAsync(sql, parameters, begin, 30, CommandType.Text);
                if (rowCount < 1)
                {
                    await begin.RollbackAsync(ct);
                }
                await begin.CommitAsync(ct);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void Update(Stock entity)
        {
            try
            {
                _context.Stocks.Where(s => s.Id == entity.Id)
                    .ExecuteUpdate(s => s
                    .SetProperty(oldValue => oldValue.Quantity, newValue => entity.Quantity)
                    .SetProperty(oldValue => oldValue.ReservedQuantity, newValue => entity.ReservedQuantity)
                    .SetProperty(oldValue => oldValue.Active, newValue => entity.Active)
                    .SetProperty(oldValue => oldValue.UpdatedAt, newValue => entity.UpdatedAt));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}