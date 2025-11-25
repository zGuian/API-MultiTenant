using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Infra.Data.Commons.Context;

namespace GearCore.Monolith.Infra.Data.Commons.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CommitAsync(CancellationToken ct = default) => await _context.SaveChangesAsync(ct);

        public void Commit() => _context.SaveChanges();
    }
}
