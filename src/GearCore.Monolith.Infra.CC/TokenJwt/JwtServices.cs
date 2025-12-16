using GearCore.Monolith.Infra.CC.TokenJwt.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GearCore.Monolith.Infra.CC.TokenJwt
{
    public class JwtServices(IConfiguration configuration) : IJwtServices
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(IApplicationUser user, IList<string> roles)
        {
            ValidateUser(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            //PESQUISAR SOBRE ARMAZENAMENTO DE SEGREDOS/SENHAS NA NUVEM (ATUALMENTE UTILIZANDO NO APPSETTINGS [NAO RECOMENDADO])
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key is not configured."));
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new(ClaimTypes.Name, user.UserName!),
                new(ClaimTypes.Email, user.Email!)
            };
            claims.AddRange(roles.Select(c => new Claim(ClaimTypes.Role, c)));

            //PESQUISAR PARA ADICIONAR REFRESH TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException(),
                Issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static void ValidateUser(IApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentException("UserName cannot be null or empty.", nameof(user.UserName));
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(user.Email));
            }
        }
    }
}
