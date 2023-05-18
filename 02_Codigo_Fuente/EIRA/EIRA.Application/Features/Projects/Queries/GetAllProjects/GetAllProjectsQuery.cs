using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQuery : IRequest<Response<List<ProjectInfoDTO>>>
    {
    }
}
