using EIRA.Application.DTOs;
using EIRA.Application.Services.API;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EIRA.Infrastructure.Services.API
{
    public class TokenService : ITokenService
    {

        public AuthenticationResponse ConstruirToken(UserInfoDTO credenciales, string jwtKey)
        {
            var claims = new List<Claim>
            {
                new Claim("userName", credenciales.UserName),
                new Claim("displayName", credenciales.DisplayName),
                new Claim("avatarURL", credenciales.AvatarURL),
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiracion,
            };
        }
    }
}
