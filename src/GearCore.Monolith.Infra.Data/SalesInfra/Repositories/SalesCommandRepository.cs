using Dapper;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Core.StockCore.Entities;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;

namespace GearCore.Monolith.Infra.Data.SalesInfra.Repositories
{
    public class SalesCommandRepository(AppDbContext context
        , ILogger<SalesCommandRepository> logger
        , IConfiguration configuration
        , ITenantProvider tenantProvider)
        : BaseCommandRepository<Sales, string>(context)
        , ISalesCommandRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<SalesCommandRepository> _logger = logger;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");

        public async Task ExecuteSaleAsync(Sales sale, IEnumerable<Stock> stocks)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var begin = await conn.BeginTransactionAsync();

            try
            {
                var rowSale = await InsertSale(sale, conn, begin);
                var rowSaleItem = await InsertSaleItem(sale, conn, begin);
                var rowUpdateSql = await UpdateStockAsync(stocks, conn, begin);

                if (rowSale < 1 && rowSaleItem < 1 && rowUpdateSql < 1)
                {
                    await begin.RollbackAsync();
                }
                await begin.CommitAsync();
            }
            catch (Exception)
            {
                await begin.RollbackAsync();
                throw;
            }
        }

        public async Task ConfirmSaleAsync(Sales sale)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var begin = await conn.BeginTransactionAsync();
            try
            {
                var sql = $@"UPDATE TB_SALES
                             SET 
                                COL_STATUS = @Status
                             WHERE 
                                COL_ID = @Id";

                var parameters = new
                {
                    sale.Status,
                    sale.Id,
                };

                var row = await conn.ExecuteAsync(sql, parameters, begin, 30, CommandType.Text);
                if (row < 1)
                {
                    await begin.RollbackAsync();
                }
                await begin.CommitAsync();
            }
            catch (Exception)
            {
                await begin.RollbackAsync();
                throw;
            }
        }

        private async Task<int> InsertSale(Sales sale, SqlConnection conn, DbTransaction begin)
        {
            _logger.LogInformation("INICIANDO REGISTRO DE SALE");
            var insertSale = $@"INSERT INTO TB_SALE
                                    VALUES(@Id, @TenantId, @UserId, @TotalAmount, @DiscountAmount, @FinalAmount, @Status, @CreatedAt, @UpdatedAt)";
            var parametersSale = new
            {
                sale.Id,
                _tenantProvider.TenantId,
                sale.UserId,
                sale.TotalAmount,
                sale.DiscountAmount,
                sale.FinalAmount,
                sale.Status,
                @CreatedAt = DateTimeOffset.UtcNow.LocalDateTime,
                @UpdatedAt = DateTimeOffset.UtcNow.LocalDateTime,
            };
            return await conn.ExecuteAsync(insertSale, parametersSale, begin, 30, CommandType.Text);
        }

        private async Task<int> InsertSaleItem(Sales sale, SqlConnection conn, DbTransaction begin)
        {
            _logger.LogInformation("INICIANDO REGISTRO DE SALE_ITEM");
            var insertSaleItem = $@"INSERT INTO TB_SALE_ITEM
                                        VALUES(@Id, @SaleId, @ProductId, @ProductName, @UnitPrice, @Quantity, @TotalPrice)";
            var parametersSaleItems = new List<object>();
            foreach (var item in sale.SaleItems)
            {
                parametersSaleItems.Add(new
                {
                    item.Id,
                    @SaleId = sale.Id,
                    @ProductId = item.Product.Id,
                    @ProductName = item.Product.Name,
                    item.UnitPrice,
                    item.Quantity,
                    item.TotalPrice,
                });
            }
            return await conn.ExecuteAsync(insertSaleItem, parametersSaleItems, begin, 30, CommandType.Text);
        }

        private async Task<int> UpdateStockAsync(IEnumerable<Stock> stocks, SqlConnection conn, DbTransaction begin)
        {
            _logger.LogInformation("INICIANDO ATUALIÇÃO DE STOCK");
            var updateStockSql = $@"UPDATE TB_STOCK
                                        SET 
                                            COL_QUANTITY = @Quantity,
                                            COL_RESERVED_QUANTITY = @ReservedQuantity
                                        WHERE COL_ID = @Id";

            var parametersUpdateStock = new List<Stock>();
            foreach (var item in stocks)
            {
                parametersUpdateStock.Add(new() { Id = item.Id, Quantity = item.Quantity, ReservedQuantity = item.ReservedQuantity });
            }
            return await conn.ExecuteAsync(updateStockSql, parametersUpdateStock, begin, 30, CommandType.Text);
        }
    }
}
