using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface ICustomFieldsCacheRepository
    {
        Task<List<CustomFieldDto>> GetAllowedFieldsFromCache();
        Task<List<string>> GetFieldsOnLoadConfigurationByProjectKeyFromCache(string projectKey);
        Task<List<string>> GetFieldsOnFollowUpReportByProjectKeyFromCache(string projectKey);
        Task<List<string>> GetFieldsOnGlobalReportByProjectKeyFromCache(string projectKey);
    }
}
