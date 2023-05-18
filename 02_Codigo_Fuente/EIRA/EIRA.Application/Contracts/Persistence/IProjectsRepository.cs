using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IProjectsRepository
    {
        Task<List<ProjectInfoDTO>> GetAllProjects();
    }
}
