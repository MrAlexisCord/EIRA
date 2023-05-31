using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using System.Web;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class IssuesService : APIRequestBaseService, IIssuesService
    {
        public IssuesService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<T> Create<T, Z>(BaseFieldsPostBodyRequest<Z> issueBody, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = issueBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue",
            }, expectJiraIssueError: true);
        }

        public async Task<T> Update<T, Z>(string issueIdOrKey, BaseFieldsPostBodyRequest<Z> issueBody, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = issueBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{issueIdOrKey}",
            }, expectJiraIssueError: true);
        }

        public async Task<T> GetIssueByIdOrKey<T>(string idOrKey, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{idOrKey}",
            });
        }

        public async Task<T> IssueByArandaNumber<T>(string arandaNumber, string projectIdOrKey)
        {
            string jqlStatement = $"project=\"{projectIdOrKey}\" AND \"Incidencia[Short text]\" ~ \"{arandaNumber}\"";
            string encodedJqlStatement = $"{HttpUtility.UrlEncode(jqlStatement)}";
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/search?fields=maxResults&maxResults=50&jql={encodedJqlStatement}",
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

        public async Task<T> GetCommentsByIssueIdOrKey<T>(string idOrKey)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue/{idOrKey}/comment",
            });
        }

        public async Task<T> GetIssuesByJQL<T>(string jqlStatement)
        {

            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/search?jql={jqlStatement}",
            });

        }
    }
}
