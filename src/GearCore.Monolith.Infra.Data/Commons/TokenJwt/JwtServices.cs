using GearCore.Monolith.Core.TenantCore.Entities.Enums;
using GearCore.Monolith.Core.UserCore.Entities;
using GearCore.Monolith.Infra.Data.Commons.TokenJwt.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GearCore.Monolith.Infra.Data.Commons.TokenJwt
{
    public class JwtServices(IConfiguration configuration) : IJwtServices
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(User user, IList<string> roles)
        {
            ValidateUser(user);
            var tokenHandler = new JwtSecurityTokenHandler();

            //PESQUISAR SOBRE ARMAZENAMENTO DE SEGREDOS/SENHAS NA NUVEM (ATUALMENTE UTILIZANDO NO APPSETTINGS [NAO RECOMENDADO])
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT Key missing."));
            var issuer = _configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException("JWT Issuer missing.");
            var audience = _configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException("JWT Audience missing.");
            var tenantId = user.TenantUsers.Single().Tenant.Id;

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.CompleteName),
                new(ClaimTypes.Email, user.Email),
                new("TenantId", tenantId),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tenantRoles = user.TenantUsers.Single().Role;
            if (tenantRoles.Contains(nameof(RoleTenantEnum.SYSTEM_ADMIN), StringComparison.OrdinalIgnoreCase))
            {
                claims.Add(new Claim(ClaimTypes.Role, nameof(RoleTenantEnum.SYSTEM_ADMIN)));
            }
            claims.Add(new Claim(ClaimTypes.Role, nameof(RoleTenantEnum.SYSTEM_USER)));
            claims.AddRange(roles.Select(c => new Claim(ClaimTypes.Role, c)));

            //PESQUISAR PARA ADICIONAR REFRESH TOKEN
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException(),
                Issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException(),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if (string.IsNullOrEmpty(user.CompleteName))
            {
                throw new ArgumentException("UserName cannot be null or empty.", nameof(user.CompleteName));
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(user.Email));
            }
        }
    }
}
