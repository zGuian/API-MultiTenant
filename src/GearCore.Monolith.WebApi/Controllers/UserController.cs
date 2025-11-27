using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using GearCore.Monolith.Infra.Data.IdentityEF.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email)
                ?? throw new UnauthorizedAccessException("Usuário não encontrado.");
            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!checkPassword.Succeeded)
            {
                throw new UnauthorizedAccessException("Senhas incorreta");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtServices.GenerateToken(user, roles);
            return Ok(new
            {
                Token = token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
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
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Falha ao criar usuário.");
            }
            return Ok(new { Message = "Usuário criado com sucesso!" });
        }
    }
}
