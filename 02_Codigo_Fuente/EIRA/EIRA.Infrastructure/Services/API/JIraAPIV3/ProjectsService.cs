using EIRA.Application.Models.External;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class ProjectsService : APIRequestBaseService, IProjectsService
    {
        public ProjectsService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<T> GetAllProjects<T>()
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/project/recent",
            });
        }

        public async Task<T>  AssignableUsersByProjectId<T>(string projectKeyOrId)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{ExternalEndpoint.JiraAPIBaseV3}/user/assignable/search?project={projectKeyOrId}",
            });
        }
    }
}
