using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Infra.Data.Commons.Context;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.Infra.Data.Commons.Repositories
{
    public abstract class BaseQueryRepository<TEntity, TKey>(AppDbContext context)
        : IBaseQueryRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _context = context.Set<TEntity>();

        public virtual async Task<HashSet<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await _context.ToHashSetAsync(ct) ?? throw new NotFoundException("No entities found");

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct = default)
            => await _context.FindAsync([id], cancellationToken: ct) ?? throw new NotFoundException("Entity not found");
    }
}
