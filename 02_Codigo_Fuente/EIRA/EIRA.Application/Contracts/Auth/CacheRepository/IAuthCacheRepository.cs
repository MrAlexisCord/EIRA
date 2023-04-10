using EIRA.Application.DTOs;
using EIRA.Application.Models.External;

namespace EIRA.Application.Contracts.Auth.CacheRepository
{
    public interface IAuthCacheRepository
    {
        Task<UserInfoDTO> GetUserInfoInCache(AuthLoginRequestBody authRequest);
    }
}
