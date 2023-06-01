using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Specifications.CustomFields;
using EIRA.Application.Wrappers;
using EIRA.Domain.EIRAEntities.App;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey
{
    public class GetFieldsFollowUpConfigurationByProjectKeyQueryHandler : IRequestHandler<GetFieldsFollowUpConfigurationByProjectKeyQuery, Response<List<ConfigurationFieldDTO>>>
    {
        private readonly IAsyncRepository<AppConfigurationFollowUpReport> _repository;
        private readonly IMapper _mapper;

        public GetFieldsFollowUpConfigurationByProjectKeyQueryHandler(IAsyncRepository<AppConfigurationFollowUpReport> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<ConfigurationFieldDTO>>> Handle(GetFieldsFollowUpConfigurationByProjectKeyQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAsync(new FollowUpCustomFieldsByProjectSpecification(projectKey: request.ProjectKey));
            var dtoResult = _mapper.Map<List<ConfigurationFieldDTO>>(result);

            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();

            //if (fieldsIds is null || !fieldsIds.Any())
            //{
            //    throw new Exception(message: $"No existe configuración de campos para este reporte en el proyecto: '{request.ProjectKey}'");
            //}

            return new Response<List<ConfigurationFieldDTO>>(dtoResult);
        }
    }
}
