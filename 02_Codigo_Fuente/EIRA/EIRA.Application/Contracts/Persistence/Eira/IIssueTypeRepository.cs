using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface IIssueTypeRepository
    {
        Task<List<IssueTypeConfigurationDTO>> GetIssueTypeConfiguration();
    }
}
