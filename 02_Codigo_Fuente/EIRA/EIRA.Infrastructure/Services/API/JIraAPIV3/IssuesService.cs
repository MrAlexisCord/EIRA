using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using EIRA.Application.Statics.Jira;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class IssuesService : APIRequestBaseService, IIssuesService
    {
        public IssuesService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<T> Create<T>(BaseFieldsPostBodyRequest<IssueCreateRequest> issueBody, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = issueBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue",
            });
        }

        public async Task<T> Update<T>(string issueIdOrKey, BaseFieldsPostBodyRequest<IssueUpdateRequest> issueBody, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = issueBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{issueIdOrKey}",
            });
        }

        public async Task<T> GetIssueByIdOrKey<T>(string idOrKey, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{idOrKey}",
            });
        }

        public async Task<T> IssueByArandaNumber<T>(string arandaNumber)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType= ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/search?fields=maxResults&maxResults=50&jql=project%3D{JiraConfiguration.ProyectoId}%20AND%20\"Incidencia%5BShort%20text%5D\"~{arandaNumber}",
            });

        }

        public async Task<T> CommentOnIssue<T>(string idOrKey, CommentOnIssueCreateRequest commentBody)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = commentBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{idOrKey}/comment",
            });
        }
    }
}
