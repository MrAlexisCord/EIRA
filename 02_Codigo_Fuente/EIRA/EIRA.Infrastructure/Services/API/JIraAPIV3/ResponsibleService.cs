using EIRA.Application.Models.External;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using EIRA.Application.Statics.Jira;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class ResponsibleService : APIRequestBaseService, IResponsibleService
    {
        public ResponsibleService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<T> GetResponsibleList<T>()
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/customField/{JiraConfiguration.ResponsableCustomFieldId}/option",
            });
        }

        public async Task<T> GetDefaultValue<T>()
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/field/customfield_{JiraConfiguration.ResponsableCustomFieldId}/context/defaultValue", // 10145
            });
        }
    }
}