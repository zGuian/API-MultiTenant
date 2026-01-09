using Dapper;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.SalesCore.Entities;
using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
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

        private DbTransaction? _begin;

        public async Task ExecuteSaleAsync(Sales sale)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                await conn.OpenAsync();
                _begin = await conn.BeginTransactionAsync();

                var insertSale = $@"INSERT INTO TB_SALES
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
                var rowSale = await conn.ExecuteAsync(insertSale, parametersSale, _begin, 30, CommandType.Text);

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
                var rowSaleItem = await conn.ExecuteAsync(insertSaleItem, parametersSaleItems, _begin, 30, CommandType.Text);

                if (rowSale < 1 && rowSaleItem < 1)
                {
                    await _begin.RollbackAsync();
                }
                await _begin.CommitAsync();
            }
            catch (Exception)
            {
                if (_begin != null)
                {
                    await _begin.RollbackAsync();
                }
                throw;
            }
        }
    }
}
