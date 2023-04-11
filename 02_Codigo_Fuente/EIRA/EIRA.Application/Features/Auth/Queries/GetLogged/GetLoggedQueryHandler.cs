using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
using EIRA.Application.Services.API;
using MediatR;

namespace EIRA.Application.Features.Auth.Queries.GetLogged
{
    public class GetLoggedQueryHandler : IRequestHandler<GetLoggedQuery, AuthenticationResponse>
    {
        private readonly IAuthCacheRepository _authCacheRepository;
        private readonly ITokenService _tokenService;

        public GetLoggedQueryHandler(IAuthCacheRepository authCacheRepository, ITokenService tokenService)
        {
            _authCacheRepository = authCacheRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> Handle(GetLoggedQuery request, CancellationToken cancellationToken)
        {
            var response = await _authCacheRepository.GetUserInfoInCache(new AuthLoginRequestBody { UserName = request.UserName, JiraApiKey = request.JiraApiKey });
            if (response is null)
            {
                throw new Exception(message: "Bad Authenticate Request");
            }

            var authResponse = _tokenService.ConstruirToken(response, request.GetApiKeyJwt());

            return authResponse;
        }
    }
}
