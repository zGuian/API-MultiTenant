using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Infra.Data.Commons.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GearCore.Monolith.Infra.Data.Commons.Repositories
{
    public abstract class BaseCommandRepository<TEntity, UId>(AppDbContext context)
        : IBaseCommandRepository<TEntity, UId> where TEntity : class
    {
        private readonly DbSet<TEntity> _context = context.Set<TEntity>();

        public virtual async Task InsertAsync(TEntity entity, CancellationToken ct = default)
        {
            try
            {
                await _context.AddAsync(entity, ct);
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine($"InsertAsync operation was canceled: {ex.Message}");
            }
        }

        public virtual void Update(TEntity entity) => _context.Update(entity);

        public virtual void Delete(UId key) => _context.Remove(_context.Find(key) ?? throw new InvalidOperationException("Entity not found for deletion."));
    }
}
