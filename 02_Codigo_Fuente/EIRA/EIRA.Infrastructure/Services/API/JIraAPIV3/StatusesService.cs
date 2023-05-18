using EIRA.Application.Models.External;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class StatusesService: APIRequestBaseService, IStatusesService
    {
        public StatusesService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<T> GetAllStatusesByProject<T>(string projectId)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/statuses/search?projectId={projectId}",
            });
        }
    }
}
