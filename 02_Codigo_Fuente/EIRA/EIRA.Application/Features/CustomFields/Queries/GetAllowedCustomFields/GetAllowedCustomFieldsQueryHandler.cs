using AutoMapper;
using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using EIRA.Domain.EIRAEntities.Bas;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetAllowedCustomFields
{
    public class GetAllowedCustomFieldsQueryHandler : IRequestHandler<GetAllowedCustomFieldsQuery, Response<List<CustomFieldDto>>>
    {
        private readonly IAsyncRepository<BasField> _repository;
        private readonly IMapper _mapper;

        public GetAllowedCustomFieldsQueryHandler(IAsyncRepository<BasField> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<CustomFieldDto>>> Handle(GetAllowedCustomFieldsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.ListAllNoTrackingAsync();
            var dtoResult = _mapper.Map<List<CustomFieldDto>>(result);
            return new Response<List<CustomFieldDto>>(dtoResult);
        }
    }
}
