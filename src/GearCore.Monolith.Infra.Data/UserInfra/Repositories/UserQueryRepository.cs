using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.Infra.Data.Commons.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GearCore.Monolith.Infra.Data.UserInfra.Repositories
{
    public class UserQueryRepository(AppDbContext context
        , ILogger<UserQueryRepository> logger
        , IConfiguration configuration)
        : BaseQueryRepository<User, string>(context), IUserQueryRepository
    {
        private readonly ILogger<UserQueryRepository> _logger = logger;
        private readonly AppDbContext _context = context;
        private readonly string _connectionString = configuration.GetConnectionString("SQLDefault")
            ?? throw new ArgumentNullException("ConnectionString:SQLDefault");

        public override async Task<User> GetByIdAsync(string id, CancellationToken ct = default)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(e => e.Id ==id, ct)
                ?? throw new NotFoundException("Entity not found");
            return entity;
        }

        public async Task<IEnumerable<User>> GetPageAsync(int pageIndex, int pageSize) 
            => await _context.Users.AsNoTracking()
                                   .Include(tu => tu.TenantUsers)
                                   .ThenInclude(t => t.Tenant)
                                   .Include(ur => ur.UserRoles)
                                   .ThenInclude(r => r.Roles)
                                   .Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

        public async Task<User?> FindByEmailAsync(string email)
            => await _context.Users.Include(tu => tu.TenantUsers)
                                   .ThenInclude(t => t.Tenant)
                                   .Include(ur => ur.UserRoles)
                                   .ThenInclude(r => r.Roles)
                                   .Where(u => u.NormalizedEmail.Contains(email.ToUpper()))
                                   .FirstOrDefaultAsync();

        public async Task<User?> FindByNameAsync(string name)
            => await _context.Users.Include(tu => tu.TenantUsers)
                                   .ThenInclude(t => t.Tenant)
                                   .Include(ur => ur.UserRoles)
                                   .ThenInclude(r => r.Roles)
                                   .Where(u => u.CompleteName.Contains(name.ToUpper()))
                                   .FirstOrDefaultAsync();

        public async Task<IList<string>> GetRolesAsync(string userID)
            => await _context.UserRoles.Where(u => u.UserID == userID)
                                       .Select(u => u.Roles.RoleName)
                                       .ToListAsync();
    }
}
