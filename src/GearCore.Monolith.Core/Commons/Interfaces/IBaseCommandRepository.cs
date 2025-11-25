
namespace GearCore.Monolith.Core.Commons.Interfaces
{
    public interface IBaseCommandRepository<TEntity, UId>
    {
        void Delete(UId key);
        Task InsertAsync(TEntity entity, CancellationToken ct = default);
        void Update(TEntity entity);
    }
}
