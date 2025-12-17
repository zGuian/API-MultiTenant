using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using Mapster;

namespace GearCore.Monolith.Core.UserCore.Converters
{
    public static class UserConverter
    {
        public static void UserConverters()
        {
            TypeAdapterConfig<UserRegisterDto, User>.NewConfig()
                .ConstructUsing(dto => new User(dto.FirstName, dto.LastName, dto.Email, dto.Password, dto.PhoneNumber));

            TypeAdapterConfig<User, UserDto>.NewConfig()
                .ConstructUsing(u => new UserDto(u.UserRoles.Select(ur => ur.Roles), u.TenantUsers.Select(tu => tu.Tenant)));
        }
    }
}
