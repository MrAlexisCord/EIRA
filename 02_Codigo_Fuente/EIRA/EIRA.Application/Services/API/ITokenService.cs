using EIRA.Application.DTOs;

namespace EIRA.Application.Services.API
{
    public interface ITokenService
    {
        AuthenticationResponse ConstruirToken(UserInfoDTO credenciales, string jwtKey);
    }
}
