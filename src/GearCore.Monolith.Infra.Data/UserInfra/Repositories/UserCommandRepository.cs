using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.UserInfra.Repositories
{
    public class UserCommandRepository(AppDbContext context
        , ILogger<UserCommandRepository> logger
        , IConfiguration configuration) 
        : BaseCommandRepository<User, string>(context), IUserCommandRepository
    {
        private readonly ILogger<UserCommandRepository> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");
    }
}
