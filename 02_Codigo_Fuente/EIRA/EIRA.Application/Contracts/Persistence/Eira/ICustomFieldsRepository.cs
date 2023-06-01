using EIRA.Application.DTOs;

namespace EIRA.Application.Contracts.Persistence.Eira
{
    public interface ICustomFieldsRepository
    {
        Task<List<CustomFieldDto>> GetAllowedFields();
        Task<List<string>> GetFieldsOnLoadConfigurationByProjectKey(string projectKey);
        Task<List<string>> GetFieldsOnFollowUpReportByProjectKey(string projectKey);
        Task<List<string>> GetFieldsOnGlobalReportByProjectKey(string projectKey);

        Task<ConfigurationFieldDTO> CreateFieldOnLoadConfiguration(ConfigurationFieldDTO configuration);
        Task<ConfigurationFieldDTO> CreateFieldFollowConfiguration(ConfigurationFieldDTO configuration);
        Task<ConfigurationFieldDTO> CreateFieldGlobalConfiguration(ConfigurationFieldDTO configuration);

        Task<bool> DeleteFieldOnLoadConfiguration(ConfigurationFieldDTO configuration);
        Task<bool> DeleteFieldFollowConfiguration(ConfigurationFieldDTO configuration);
        Task<bool> DeleteFieldGlobalConfiguration(ConfigurationFieldDTO configuration);

    }
}
