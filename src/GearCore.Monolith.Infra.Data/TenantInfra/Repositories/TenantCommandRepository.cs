using Dapper;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Interfaces.Entities;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Repositories
{
    public class TenantCommandRepository(AppDbContext context
        , ILogger<TenantCommandRepository> logger
        , IConfiguration configuration)
        : BaseCommandRepository<Tenant, string>(context), ITenantCommandRepository
    {
        private readonly ILogger<TenantCommandRepository> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLServer");

        public async Task LinkToUserAsync(IApplicationUser user, string tenantID)
        {
            try
            {
                string sql = @$"INSERT INTO (userID, tenantID) VALUES (@USER_ID, @TENANT_ID)";
                var parameters = new
                {
                    USER_ID = user.Id,
                    TENANT_ID = tenantID,
                };

                await using var conn = new SqlConnection(_connectionString);
                var bt = await conn.BeginTransactionAsync();
                var line = await conn.ExecuteAsync(sql, parameters, commandTimeout: 30, commandType: CommandType.Text);
                if (line < 1)
                {
                    throw new DbUpdateException("Ocorreu um problema no banco de dados. ");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RegisterAsync(Tenant entity)
        {
            _logger.LogInformation("INICIANDO ACESSO COM BANCO DE DADOS.");
            try
            {
                var sql = $@"INSERT INTO TB_TENANT (COL_ID, COL_NAME, COL_SUBDOMAIN, COL_IS_ACTIVE) 
                             VALUES (@ID, @NAME, @SUBDOMAIN, @ISACTIVE) ";
                var parameters = new
                {
                    ID = Guid.NewGuid().ToString(),
                    NAME = entity.Name,
                    SUBDOMAIN = entity.Subdomain,
                    ISACTIVE = entity.Active
                };

                await using var conn = new SqlConnection(_connectionString);
                var line = await conn.ExecuteAsync(sql, parameters, commandTimeout: 30, commandType: CommandType.Text);
                if (line < 1)
                {
                    throw new DbUpdateException("Ocorreu um problema no banco de dados. ");
                }

                _logger.LogInformation("REGISTRO REALIZADO COM SUCESSO. FECHANDO CONEXÃO COM BANCO DE DADOS!");
                return;
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
    }
}
