using EIRA.Application.DTOs;
using EIRA.Application.Models.External;

namespace EIRA.Application.Contracts.Auth
{
    public interface IAuthJiraRepository
    {
        Task<UserInfoDTO> Login(AuthLoginRequestBody authRequest);
    }
}
