using EIRA.Application.Contracts.Persistence;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, Response<List<ProjectInfoDTO>>>
    {
        private readonly IProjectsRepository _projectsRepository;

        public GetAllProjectsQueryHandler(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        public async Task<Response<List<ProjectInfoDTO>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var response = await _projectsRepository.GetAllProjects();
            return new Response<List<ProjectInfoDTO>>(response);
        }
    }
}
