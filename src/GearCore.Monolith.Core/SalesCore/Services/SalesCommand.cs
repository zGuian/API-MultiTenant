using GearCore.Monolith.Core.SalesCore.Interfaces.Repositories;
using GearCore.Monolith.Core.SalesCore.Interfaces.Services;

namespace GearCore.Monolith.Core.SalesCore.Services
{
    public class SalesCommand(ISalesCommandRepository commandRepository) : ISalesCommand
    {
        private readonly ISalesCommandRepository _commandRepository = commandRepository;

        public async Task ExecutesSale()
        {

        }
    }
}
