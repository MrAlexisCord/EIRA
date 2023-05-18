using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class StatusesRepository : IStatusesRepository
    {
        private readonly IStatusesService _statusesService;
        private readonly IMapper _mapper;

        public StatusesRepository(IStatusesService statusesService, IMapper mapper)
        {
            _statusesService = statusesService;
            _mapper = mapper;
        }

        public async Task<List<StatusDTO>> GetStatusesByProjectId(string projectId)
        {
            var statusesList = new List<StatusDTO>();
            var response = await _statusesService.GetAllStatusesByProject<StatusesResponse>(projectId);
            if (response is not null && response.Total > 0)
            {
                var statuses = _mapper.Map<List<StatusDTO>>(response.Values);
                if (statuses != null && statuses.Any())
                {
                    statusesList.AddRange(statuses);
                }
            }
            return statusesList;
        }
    }
}
