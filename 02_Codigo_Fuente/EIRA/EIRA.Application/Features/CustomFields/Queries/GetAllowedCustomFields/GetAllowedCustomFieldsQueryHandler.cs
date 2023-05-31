using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetAllowedCustomFields
{
    public class GetAllowedCustomFieldsQueryHandler : IRequestHandler<GetAllowedCustomFieldsQuery, Response<List<CustomFieldDto>>>
    {
        private readonly ICustomFieldsRepository _customFieldsRepository;

        public GetAllowedCustomFieldsQueryHandler(ICustomFieldsRepository customFieldsRepository)
        {
            _customFieldsRepository = customFieldsRepository;
        }

        public async Task<Response<List<CustomFieldDto>>> Handle(GetAllowedCustomFieldsQuery request, CancellationToken cancellationToken)
        {
            var result = await _customFieldsRepository.GetAllowedFields();
            return new Response<List<CustomFieldDto>>(result);
        }
    }
}
