using GearCore.Monolith.Core.TenantCore.Entities.Enums;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Infra.Data.Commons.TokenJwt.Interfaces;
using GearCore.Monolith.WebApi.Attributes;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [Authorize(Roles = nameof(RoleTenantEnum.SYSTEM_USER))]
    [ApiController]
    [RequireTenant]
    [Route("api/v1/[controller]")]
    public class UserController(IUserCommand command
        , IUserQuery query
        , IJwtServices jwtServices) : ControllerBase
    {
        private readonly IJwtServices _jwtServices = jwtServices;
        private readonly IUserCommand _command = command;
        private readonly IUserQuery _query = query;

        [HttpGet(template: "filter={nameOrEmail}")]
        public async Task<IActionResult> GetByFilter([FromServices] IMapper mapper, [FromRoute] string nameOrEmail)
        {
            User? user;
            var isEmail = nameOrEmail.Contains('@');

            if (isEmail)
            {
                user = await _query.FindByEmailAsync(nameOrEmail);
            }
            else
            {
                user = await _query.FindByNameAsync(nameOrEmail);
            }

            if (user != null)
            {
                var dto = mapper.Map<UserDto>(user);
                return Ok(dto);
            }
            return NotFound("Não encontrado nenhum valor");
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 15)
        {
            var users = await _query.GetPageAsync(pageIndex, pageSize);
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromHeader] string tenantId, [FromBody] UserRegisterDto dto, CancellationToken ct)
        {
            var userExists = await _query.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {
                throw new UnauthorizedAccessException("Usuário já existe.");
            }

            var result = await _command.CreateAsync(dto, tenantId, ct);
            if (!result)
            {
                throw new UnauthorizedAccessException("Falha ao criar usuário.");
            }

            return Ok(new { Message = "Usuário criado com sucesso!" });
        }
    }
}
