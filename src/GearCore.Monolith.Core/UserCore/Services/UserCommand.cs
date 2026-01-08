using GearCore.Monolith.Core.Commons.Interfaces;
using GearCore.Monolith.Core.TenantCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Repositories;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using MapsterMapper;

namespace GearCore.Monolith.Core.UserCore.Services
{
    public class UserCommand(IUserCommandRepository commandRepository
        , ITenantCommandRepository tenantCommandRepository
        , IUnitOfWork unitOfWork
        , IMapper mapper) : IUserCommand
    {
        private readonly IUserCommandRepository _commandRepository = commandRepository;
        private readonly ITenantCommandRepository _tenantCommandRepository = tenantCommandRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> CreateAsync(UserRegisterDto dto, string tenantID, CancellationToken ct = default)
        {
            var user = _mapper.Map<User>(dto);
            if (user != null)
            {
                await _commandRepository.InsertAsync(user, ct);
                await _tenantCommandRepository.LinkToUserAsync(user, tenantID, ct);
                await _unitOfWork.CommitAsync(ct);
                return true;
            }
            return false;
        }
    }
}
