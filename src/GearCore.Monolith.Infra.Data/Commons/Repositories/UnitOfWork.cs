using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Infra.Data.Commons.Context;

namespace GearCore.Monolith.Infra.Data.Commons.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CommitAsync(CancellationToken ct = default)
        {
            try
            {
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public void Commit() => _context.SaveChanges();
    }
}
