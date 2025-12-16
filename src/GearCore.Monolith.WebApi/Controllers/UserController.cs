using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.TenantCore.Interfaces.Services;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(UserManager<ApplicationUser> userManager
        , SignInManager<ApplicationUser> signInManager
        , IJwtServices jwtServices) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IJwtServices _jwtServices = jwtServices;

        [HttpGet(template: "filter={nameOrEmail:alpha}")]
        public async Task<IActionResult> GetByFilter([FromRoute] string nameOrEmail)
        {
            ApplicationUser user;
            var isEmail = nameOrEmail.Contains('@');

            if (isEmail)
            {
                user = await _userManager.Users
                    .Where(u => u.NormalizedEmail!.Contains(nameOrEmail, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync() ?? throw new NotFoundException("NÃO FOI ENCONTRADO NENHUM EMAIL CADASTRADO.");
            }
            else
            {
                user = await _userManager.Users
                    .Where(u => u.NormalizedUserName!.Contains(nameOrEmail, StringComparison.InvariantCultureIgnoreCase))
                    .FirstOrDefaultAsync() ?? throw new NotFoundException("NÃO FOI ENCONTRADO NENHUM NOME REGISTRADO.");
            }

            var dto = user.Adapt<UserDto>();
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var users = _userManager.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new UnauthorizedAccessException("Usuário não encontrado.");
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!checkPassword.Succeeded)
            {
                throw new UnauthorizedAccessException("Senha incorreta");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtServices.GenerateToken(user, roles);
            var refreshToken = Guid.NewGuid().ToString();
            Response.Headers.Append("X-Auth-AccessToken", token);
            Response.Headers.Append("X-Auth-RefreshToken", refreshToken);
            return RedirectPermanent("/");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromServices] ITenantCommand tenantCommand,
            [FromHeader] string tenantId, [FromBody] UserRegisterDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {
                throw new UnauthorizedAccessException("Usuário já existe.");
            }
            ApplicationUser user = new()
            {
                Email = dto.Email,
                UserName = dto.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Falha ao criar usuário.");
            }

            await tenantCommand.LinkToUserAsync(user, tenantId);

            return Ok(new { Message = "Usuário criado com sucesso!" });
        }
    }
}
