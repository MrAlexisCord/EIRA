using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly IProjectsService _projectsService;
        private readonly IMapper _mapper;

        public ProjectsRepository(IProjectsService projectsService, IMapper mapper)
        {
            _projectsService = projectsService;
            _mapper = mapper;
        }

        public async Task<List<ProjectInfoDTO>> GetAllProjects()
        {
            var projectList = new List<ProjectInfoDTO>();

            var response = await _projectsService.GetAllProjects<List<ProjectsAllResponse>>();

            if(response is not null && response.Any())
            {
                var projects = _mapper.Map<List<ProjectInfoDTO>>(response);
                projectList.AddRange(projects.OrderBy(x => x.Name));
            }

            return projectList;
        }
    }
}
