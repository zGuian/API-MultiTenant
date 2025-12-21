using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.SalesCore.Entities;

namespace GearCore.Monolith.Core.SalesCore.Interfaces.Repositories
{
    public interface ISalesCommandRepository : IBaseCommandRepository<Sales, string>
    {
    }
}
