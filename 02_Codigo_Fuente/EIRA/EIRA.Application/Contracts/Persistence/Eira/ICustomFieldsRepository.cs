using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface ICustomFieldsRepository
    {
        Task<List<CustomFieldDto>> GetAllowedFields();
        Task<List<string>> GetFieldsOnLoadConfigurationByProjectKey(string projectKey);
        Task<List<string>> GetFieldsOnFollowUpReportByProjectKey(string projectKey);
        Task<List<string>> GetFieldsOnGlobalReportByProjectKey(string projectKey);
    }
}
