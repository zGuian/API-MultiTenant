using GearCore.Monolith.Core.TenantCore.DTOs;
using GearCore.Monolith.Core.TenantCore.Entities;
using GearCore.Monolith.Core.UserCore.Entities;
using Mapster;

namespace GearCore.Monolith.Core.UserCore.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string NormalizedFirstName { get; set; } = string.Empty;
        public string NormalizedLastName { get; set; } = string.Empty;
        public string CompleteName { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public IEnumerable<RoleDto> Roles { get; set; } = [];
        public IEnumerable<TenantViewDto> Tenants { get; set; } = [];

        public UserDto()
        { }

        public UserDto(IEnumerable<Roles> roles, IEnumerable<Tenant> tenants)
        {
            Roles = roles.Adapt<IEnumerable<RoleDto>>();
            Tenants = tenants.Adapt<IEnumerable<TenantViewDto>>();
        }
    }
}
