using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IStatusesRepository
    {
        Task<List<StatusDTO>> GetStatusesByProjectId(string projectId);
    }
}
