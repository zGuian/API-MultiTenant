using GearCore.Monolith.Core.Exceptions;
using GearCore.Monolith.Core.UserCore.DTOs;
using GearCore.Monolith.Core.UserCore.Interfaces.Services;
using GearCore.Monolith.Infra.Data.Commons.TokenJwt.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GearCore.Monolith.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromServices] IUserQuery query, [FromServices] IJwtServices jwtServices,
            [FromBody] UserLoginDto dto)
        {
            var user = await query.FindByEmailAsync(dto.Email) ?? throw new NotFoundException("Email inválido!");
            var checkPassword = query.CheckPasswordSignIn(user, dto.Password);
            if (!checkPassword)
            {
                throw new UnauthorizedAccessException("Senha incorreta");
            }
            var roles = await query.GetRolesAsync(user);
            var token = jwtServices.GenerateToken(user, roles);
            var refreshToken = Guid.NewGuid().ToString();
            Response.Headers.Append("X-Auth-AccessToken", token);
            Response.Headers.Append("X-Auth-RefreshToken", refreshToken);
            return Ok(new
            {
                FirstName = user.NormalizedFirstName,
                LastName = user.NormalizedLastName,
                RefreshToken = refreshToken,
                AccessToken = token
            });
        }
    }
}
