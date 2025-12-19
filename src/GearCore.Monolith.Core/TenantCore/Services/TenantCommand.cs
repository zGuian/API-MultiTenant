using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.Core.UserCore.Entities;
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

        public async Task RegisterAsync(TenantRegisterDto dto, CancellationToken ct = default)
        {
            var entity = dto.Adapt<Tenant>();
            await _commandRepository.RegisterAsync(entity, ct);
        }

        public async Task LinkToUserAsync(User user, string tenantID, CancellationToken ct = default)
        {
            await _commandRepository.LinkToUserAsync(user, tenantID, ct);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task LinkToUserAsync(string userID, CancellationToken ct = default)
        {
            await _commandRepository.LinkToUserAsync(userID, ct);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
