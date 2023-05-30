using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetAllowedCustomFields
{
    public class GetAllowedCustomFieldsQuery : IRequest<Response<List<CustomFieldDto>>>
    {

    }
}
