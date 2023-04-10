using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Models.External;
using MediatR;

namespace EIRA.Application.Features.Auth.Queries.GetLogged
{
    public class GetLoggedQueryHandler : IRequestHandler<GetLoggedQuery, object>
    {
        private readonly IAuthCacheRepository _authCacheRepository;

        public GetLoggedQueryHandler(IAuthCacheRepository authCacheRepository)
        {
            _authCacheRepository = authCacheRepository;
        }

        public async Task<object> Handle(GetLoggedQuery request, CancellationToken cancellationToken)
        {
            var response = await _authCacheRepository.GetUserInfoInCache(new AuthLoginRequestBody { UserName = request.UserName, JiraApiKey = request.JiraApiKey });
            return response;
        }
    }
}
