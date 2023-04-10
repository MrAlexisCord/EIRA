using EIRA.Application.Contracts.Auth;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
using EIRA.Application.Statics.CacheKeys;
using Microsoft.Extensions.Caching.Memory;

namespace EIRA.Infrastructure.Repositories.Auth.CacheRepository
{
    public class AuthCacheRepository: IAuthCacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAuthJiraRepository _authRepository;

        public AuthCacheRepository(IMemoryCache memoryCache, IAuthJiraRepository authRepository)
        {
            _memoryCache = memoryCache;
            _authRepository = authRepository;
        }

        public async Task<UserInfoDTO> GetUserInfoInCache(AuthLoginRequestBody authRequest)
        {
            return await _memoryCache.GetOrCreateAsync(AuthCacheKeys.USER_INFO, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(7);
                return await _authRepository.Login(authRequest);
            });
        }


    }
}
