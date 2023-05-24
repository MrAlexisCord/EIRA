using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics.CacheKeys;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IProjectsService _projectsService;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public ProjectsRepository(IProjectsService projectsService, IMapper mapper, ICacheService cacheService)
        {
            _projectsService = projectsService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<List<ProjectInfoDTO>> GetAllProjects()
        {
            var userInfo = _cacheService.GetByKey<UserInfoDTO>(AuthCacheKeys.USER_INFO);
            var projectList = new List<ProjectInfoDTO>();

            var response = await _projectsService.GetAllProjects<List<ProjectsAllResponse>>();


            if (response is not null && response.Any())
            {
                var projects = _mapper.Map<List<ProjectInfoDTO>>(response);
                projectList.AddRange(projects.OrderBy(x => x.Name));
            }


            foreach (var project in projectList)
            {
                var responseUsers = await _projectsService.AssignableUsersByProjectId<List<AssignableUsersByProjectResponse>>(project.Key);
                if (responseUsers is null || !responseUsers.Any(x => x.AccountId == userInfo.AccountId))
                {
                    projectList = projectList?.Where(x => x.Id != project.Id)?.ToList();
                }
            }

            return projectList;
        }
    }
}
