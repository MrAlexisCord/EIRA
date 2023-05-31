using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.Specifications.CustomFields;
using EIRA.Application.Wrappers;
using EIRA.Domain.EIRAEntities.App;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey
{
    public class GetFieldsOnLoadConfigurationByProjectKeyQueryHandler : IRequestHandler<GetFieldsOnLoadConfigurationByProjectKeyQuery, Response<List<string>>>
    {
        private readonly IAsyncRepository<AppConfigurationLoadInformation> _repository;
        private readonly IMapper _mapper;

        public GetFieldsOnLoadConfigurationByProjectKeyQueryHandler(IAsyncRepository<AppConfigurationLoadInformation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<string>>> Handle(GetFieldsOnLoadConfigurationByProjectKeyQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAsync(new CustomFieldsByProjectKeySpecification(projectKey: request.ProjectKey));
            var fieldsIds = result?.Select(x => x.FieldId)?.ToList();
            return new Response<List<string>>(fieldsIds);
        }
    }
}
