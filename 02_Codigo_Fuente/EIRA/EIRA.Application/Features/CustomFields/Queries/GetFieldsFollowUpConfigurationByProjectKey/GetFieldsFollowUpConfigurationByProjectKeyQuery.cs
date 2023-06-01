using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey
{
    public class GetFieldsFollowUpConfigurationByProjectKeyQuery : IRequest<Response<List<ConfigurationFieldDTO>>>
    {
        public string ProjectKey { get; set; }
    }
}
