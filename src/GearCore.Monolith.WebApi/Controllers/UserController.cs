using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _query.FindByEmailAsync(dto.Email) ?? throw new NotFoundException("Email inválido!");
            var checkPassword = _query.CheckPasswordSignIn(user, dto.Password);
            if (!checkPassword)
            {
                throw new UnauthorizedAccessException("Senha incorreta");
            }
            var roles = await _query.GetRolesAsync(user);
            var token = _jwtServices.GenerateToken(user, roles);
            var refreshToken = Guid.NewGuid().ToString();
            Response.Headers.Append("X-Auth-AccessToken", token);
            Response.Headers.Append("X-Auth-RefreshToken", refreshToken);
            return Ok(token);
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
