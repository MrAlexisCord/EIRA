using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.Statuses.Queries.GetStatusesByProjectId
{
    public class GetStatusesByProjectIdQuery: IRequest<Response<List<StatusDTO>>>
    {
        public string ProjectId { get; set; }
    }
}
