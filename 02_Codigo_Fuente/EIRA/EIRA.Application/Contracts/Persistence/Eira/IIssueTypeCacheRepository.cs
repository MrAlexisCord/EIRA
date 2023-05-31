using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface IIssueTypeCacheRepository
    {
        Task<List<IssueTypeConfigurationDTO>> GetIssueTypeConfigurationFromCache();
    }
}
