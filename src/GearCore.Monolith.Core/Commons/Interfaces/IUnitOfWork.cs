using System.Net.Http.Headers;

namespace GearCore.Monolith.Core.Commons.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken ct = default);
        void Commit();  
    }
}
