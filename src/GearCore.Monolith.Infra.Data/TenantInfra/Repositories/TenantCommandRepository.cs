using Dapper;
using GearCore.Monolith.Core.Commons.Tenacy.Interfaces;
using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace GearCore.Monolith.Infra.Data.TenantInfra.Repositories
{
    public class TenantCommandRepository(AppDbContext context
        , ILogger<TenantCommandRepository> logger
        , ITenantProvider tenantProvider
        , IConfiguration configuration)
        : BaseCommandRepository<Tenant, string>(context), ITenantCommandRepository
    {
        private readonly ILogger<TenantCommandRepository> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");
        private readonly ITenantProvider _tenantProvider = tenantProvider;

        public async Task LinkToUserAsync(User user, string tenantID, CancellationToken ct = default)
        {
            //HÁ UM PROCESO DE "round-trip" QUE PODE CAUSAR LENTIDÃO EM GRANDE ESCALA.
            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Id == tenantID, ct)
                ?? throw new NotImplementedException();

            var tenantUser = new TenantUser(user, tenant);
            await _context.TenantUsers.AddAsync(tenantUser, ct);
        }

        public async Task LinkToUserAsync(string userID, CancellationToken ct = default)
        {
            //HÁ UM PROCESO DE "round-trip" QUE PODE CAUSAR LENTIDÃO EM GRANDE ESCALA.
            try
            {
                var tupleResult = await _context.Users.Where(u => u.Id == userID)
                                          .Select(u => new
                                          {
                                              User = u,
                                              Tenant = _context.Tenants.FirstOrDefault(t => t.Id == _tenantProvider.TenantId)
                                          })
                                          .FirstOrDefaultAsync(ct);

                var user = tupleResult?.User;
                var tenant = tupleResult?.Tenant;

                if (user != null && tenant != null)
                {
                    var tenantUser = new TenantUser(user, tenant);
                    await _context.TenantUsers.AddAsync(tenantUser, ct);
                    return;
                }
                throw new NotFoundException("Ocorreu um problema. Não foi encontrado paramentros no banco de dados");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RegisterAsync(Tenant entity, CancellationToken ct = default)
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
                    ISACTIVE = entity.IsActive
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
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}
