
namespace GearCore.Monolith.Core.Commons.Interfaces
{
    public interface IBaseQueryRepository<TEntity, UId>
    {
        Task<HashSet<TEntity>> GetAllAsync(CancellationToken ct = default);
        Task<TEntity> GetByIdAsync(UId id, CancellationToken ct = default);
    }
}
