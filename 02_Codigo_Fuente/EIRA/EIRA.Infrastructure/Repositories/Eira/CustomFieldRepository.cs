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
        private readonly IMapper _mapper;

        public CustomFieldsRepository(IAsyncRepository<BasField> repository, IMapper mapper, IAsyncRepository<AppConfigurationLoadInformation> repositoryLoadConfiguration)
        {
            _repository = repository;
            _mapper = mapper;
            _repositoryLoadConfiguration = repositoryLoadConfiguration;
        }

        public async Task<List<CustomFieldDto>> GetAllowedFields()
        {
            var result = await _repository.ListAllNoTrackingAsync();
            var dtoResult = _mapper.Map<List<CustomFieldDto>>(result);
            return dtoResult;
        }

        public Task<List<string>> GetFieldsOnFollowUpReportByProjectKey(string projectKey)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetFieldsOnGlobalReportByProjectKey(string projectKey)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetFieldsOnLoadConfigurationByProjectKey(string projectKey)
        {
            var result = await _repositoryLoadConfiguration.ListAsync(new CustomFieldsByProjectKeySpecification(projectKey: projectKey));
            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();
            return fieldsIds;
        }
    }
}
