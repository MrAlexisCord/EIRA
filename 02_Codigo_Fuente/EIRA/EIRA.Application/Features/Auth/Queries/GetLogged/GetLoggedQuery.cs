using EIRA.Application.DTOs;
using MediatR;

namespace EIRA.Application.Features.Auth.Queries.GetLogged
{
    public class GetLoggedQuery : IRequest<AuthenticationResponse>
    {
        public string UserName { get; set; }
        public string JiraApiKey { get; set; }

        private string ApiKeyJwt { get; set; }

        public void SetApiKeyJwt(string apiKeyJwt)
        {
            ApiKeyJwt = apiKeyJwt;
        }

        public string GetApiKeyJwt()
        {
            return this.ApiKeyJwt;
        }
    }
}
