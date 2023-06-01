using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Specifications.CustomFields;
using EIRA.Application.Wrappers;
using EIRA.Domain.EIRAEntities.App;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey
{
    public class GetFieldsOnLoadConfigurationByProjectKeyQueryHandler : IRequestHandler<GetFieldsOnLoadConfigurationByProjectKeyQuery, Response<List<ConfigurationFieldDTO>>>
    {
        private readonly IAsyncRepository<AppConfigurationLoadInformation> _repository;
        private readonly IMapper _mapper;

        public GetFieldsOnLoadConfigurationByProjectKeyQueryHandler(IAsyncRepository<AppConfigurationLoadInformation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<ConfigurationFieldDTO>>> Handle(GetFieldsOnLoadConfigurationByProjectKeyQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAsync(new CustomFieldsByProjectKeySpecification(projectKey: request.ProjectKey));
            var dtoResult = _mapper.Map<List<ConfigurationFieldDTO>>(result);

            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();

            //if (fieldsIds is null || !fieldsIds.Any())
            //{
            //    throw new Exception(message: $"No existe configuración de campos para la carga de incidentes en el proyecto: '{request.ProjectKey}'");
            //}
            return new Response<List<ConfigurationFieldDTO>>(dtoResult);
        }
    }
}
