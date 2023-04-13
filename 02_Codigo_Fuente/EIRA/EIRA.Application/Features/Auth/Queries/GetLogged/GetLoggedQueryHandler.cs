using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
using EIRA.Application.Services;
using EIRA.Application.Services.API;
using MediatR;

namespace EIRA.Application.Features.Auth.Queries.GetLogged
{
    public class GetLoggedQueryHandler : IRequestHandler<GetLoggedQuery, AuthenticationResponse>
    {
        private readonly IAuthCacheRepository _authCacheRepository;
        private readonly ITokenService _tokenService;
        private readonly ICacheService _cacheService;

        public GetLoggedQueryHandler(IAuthCacheRepository authCacheRepository, ITokenService tokenService, ICacheService cacheService)
        {
            _authCacheRepository = authCacheRepository;
            _tokenService = tokenService;
            _cacheService = cacheService;
        }

        public async Task<AuthenticationResponse> Handle(GetLoggedQuery request, CancellationToken cancellationToken)
        {
            _cacheService.ClearAllCachingMemory();
            var response = await _authCacheRepository.GetUserInfoInCache(new AuthLoginRequestBody { UserName = request.UserName, JiraApiKey = request.JiraApiKey });
            if (response is null)
            {
                throw new Exception(message: "Imposible autenticarse");
            }

            var authResponse = _tokenService.ConstruirToken(response, request.GetApiKeyJwt());

            return authResponse;
        }
    }
}
