using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey
{
    public class GetFieldsOnLoadConfigurationByProjectKeyQuery : IRequest<Response<List<string>>>
    {
        public string ProjectKey { get; set; }
    }
}
