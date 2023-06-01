using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Specifications.CustomFields;
using EIRA.Domain.EIRAEntities.App;
using EIRA.Domain.EIRAEntities.Bas;

namespace EIRA.Infrastructure.Repositories.Eira
{
    public class CustomFieldsRepository : ICustomFieldsRepository
    {
        private readonly IAsyncRepository<BasField> _repository;
        private readonly IAsyncRepository<AppConfigurationLoadInformation> _repositoryLoadConfiguration;
        private readonly IAsyncRepository<AppConfigurationFollowUpReport> _repositoryFollowUpConfiguration;
        private readonly IAsyncRepository<AppConfigurationGlobalReport> _repositoryGlobalConfiguration;
        private readonly IMapper _mapper;

        public CustomFieldsRepository(IAsyncRepository<BasField> repository, IMapper mapper, IAsyncRepository<AppConfigurationLoadInformation> repositoryLoadConfiguration, IAsyncRepository<AppConfigurationFollowUpReport> repositoryFollowUpConfiguration, IAsyncRepository<AppConfigurationGlobalReport> repositoryGlobalConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryLoadConfiguration = repositoryLoadConfiguration;
            _repositoryFollowUpConfiguration = repositoryFollowUpConfiguration;
            _repositoryGlobalConfiguration = repositoryGlobalConfiguration;
        }

        public async Task<ConfigurationFieldDTO> CreateFieldFollowConfiguration(ConfigurationFieldDTO configuration)
        {
            var payload = _mapper.Map<AppConfigurationFollowUpReport>(configuration);
            var response = await _repositoryFollowUpConfiguration.AddAsync(payload);
            return _mapper.Map<ConfigurationFieldDTO>(response);
        }

        public async Task<ConfigurationFieldDTO> CreateFieldGlobalConfiguration(ConfigurationFieldDTO configuration)
        {
            var payload = _mapper.Map<AppConfigurationGlobalReport>(configuration);
            var response = await _repositoryGlobalConfiguration.AddAsync(payload);
            return _mapper.Map<ConfigurationFieldDTO>(response);
        }

        public async Task<ConfigurationFieldDTO> CreateFieldOnLoadConfiguration(ConfigurationFieldDTO configuration)
        {
            var payload = _mapper.Map<AppConfigurationLoadInformation>(configuration);
            var response = await _repositoryLoadConfiguration.AddAsync(payload);
            return _mapper.Map<ConfigurationFieldDTO>(response);
        }

        public async Task<bool> DeleteFieldFollowConfiguration(ConfigurationFieldDTO configuration)
        {
            var record = (await _repositoryFollowUpConfiguration.ListNotTrackingAsync(new FollowUpCustomFieldsByProjectSpecification(configuration.ProjectId, configuration.FieldId)))?.FirstOrDefault();
            if (record is null)
            {
                throw new KeyNotFoundException(message: "El campo que desea quitar no existe");
            }

            await _repositoryFollowUpConfiguration.DeleteAsync(record);
            return true;
        }

        public async Task<bool> DeleteFieldGlobalConfiguration(ConfigurationFieldDTO configuration)
        {
            var record = (await _repositoryGlobalConfiguration.ListNotTrackingAsync(new GlobalCustomFieldsByProjectSpecification(configuration.ProjectId, configuration.FieldId)))?.FirstOrDefault();
            if (record is null)
            {
                throw new KeyNotFoundException(message: "El campo que desea quitar no existe");
            }

            await _repositoryGlobalConfiguration.DeleteAsync(record);
            return true;
        }

        public async Task<bool> DeleteFieldOnLoadConfiguration(ConfigurationFieldDTO configuration)
        {
            var record = (await _repositoryLoadConfiguration.ListNotTrackingAsync(new CustomFieldsByProjectKeySpecification(configuration.ProjectId, configuration.FieldId)))?.FirstOrDefault();
            if (record is null)
            {
                throw new KeyNotFoundException(message: "El campo que desea quitar no existe");
            }

            await _repositoryLoadConfiguration.DeleteAsync(record);
            return true;
        }

        public async Task<List<CustomFieldDto>> GetAllowedFields()
        {
            var result = await _repository.ListAllNoTrackingAsync();
            var dtoResult = _mapper.Map<List<CustomFieldDto>>(result);
            return dtoResult;
        }

        public async Task<List<string>> GetFieldsOnFollowUpReportByProjectKey(string projectKey)
        {
            var result = await _repositoryFollowUpConfiguration.ListAsync(new FollowUpCustomFieldsByProjectSpecification(projectKey: projectKey));
            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();
            return fieldsIds;
        }

        public async Task<List<string>> GetFieldsOnGlobalReportByProjectKey(string projectKey)
        {
            var result = await _repositoryGlobalConfiguration.ListAsync(new GlobalCustomFieldsByProjectSpecification(projectKey: projectKey));
            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();
            return fieldsIds;
        }

        public async Task<List<string>> GetFieldsOnLoadConfigurationByProjectKey(string projectKey)
        {
            var result = await _repositoryLoadConfiguration.ListAsync(new CustomFieldsByProjectKeySpecification(projectKey: projectKey));
            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();
            return fieldsIds;
        }
    }
}
