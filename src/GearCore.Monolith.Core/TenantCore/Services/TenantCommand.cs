using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using Mapster;

namespace GearCore.Monolith.Core.TenantCore.Services
{
    public class TenantCommand(ITenantCommandRepository tenantCommandRepository
        , ITenantQueryRepository tenantQueryRepository
        , IUnitOfWork unitOfWork) : ITenantCommand
    {
        private readonly ITenantCommandRepository _commandRepository = tenantCommandRepository;
        private readonly ITenantQueryRepository _queryRepository = tenantQueryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task RegisterAsync(TenantRegisterDto dto)
        {
            var entity = dto.Adapt<Tenant>();
            await _commandRepository.RegisterAsync(entity);
        }

        public async Task LinkToUserAsync(IApplicationUser user, string tenantID)
        {
            await _commandRepository.LinkToUserAsync(user, tenantID);
        }
    }
}
