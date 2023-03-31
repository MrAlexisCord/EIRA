using DocumentFormat.OpenXml.Office2010.Excel;
using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class IssuesService : APIRequestBaseService, IIssuesService
    {
        public IssuesService(IHttpClientFactory httpClient) : base(httpClient)
        {

        }

        public async Task<T> Create<T>(BaseFieldsPostBodyRequest<IssueCreateRequest> issueBody, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = issueBody,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/issue",
                AccessToken = token,
            });
        }

        public async Task<T> GetIssueByIdOrKey<T>(string idOrKey, string token)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/{idOrKey}",
                AccessToken = token,
            });
        }
    }
}
