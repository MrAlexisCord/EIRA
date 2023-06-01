using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey
{
    public class GetFieldsOnLoadConfigurationByProjectKeyQuery : IRequest<Response<List<ConfigurationFieldDTO>>>
    {
        public string ProjectKey { get; set; }
    }
}
