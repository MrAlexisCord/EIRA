using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.Statuses.Queries.GetStatusesByProjectId
{
    public class GetStatusesByProjectIdQueryHandler : IRequestHandler<GetStatusesByProjectIdQuery, Response<List<StatusDTO>>>
    {
        private readonly IStatusesRepository _statusesRepository;

        public GetStatusesByProjectIdQueryHandler(IStatusesRepository statusesRepository)
        {
            _statusesRepository = statusesRepository;
        }

        public async Task<Response<List<StatusDTO>>> Handle(GetStatusesByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _statusesRepository.GetStatusesByProjectId(request.ProjectId);
            return new Response<List<StatusDTO>>(response?.OrderBy(x => x.Name)?.ToList());
        }
    }
}
