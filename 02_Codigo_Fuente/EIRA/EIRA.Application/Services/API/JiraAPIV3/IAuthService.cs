using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(AuthLoginRequestBody authRequest);
    }
}
