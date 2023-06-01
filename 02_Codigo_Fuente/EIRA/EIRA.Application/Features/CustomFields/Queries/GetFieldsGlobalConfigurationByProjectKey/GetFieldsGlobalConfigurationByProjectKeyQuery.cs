using EIRA.Application.DTOs;
using EIRA.Application.Wrappers;
using EIRA.Domain.EIRAEntities.App;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsGlobalConfigurationByProjectKey
{
    public class GetFieldsGlobalConfigurationByProjectKeyQuery : IRequest<Response<List<ConfigurationFieldDTO>>>
    {
        public string ProjectKey { get; set; }
    }
}
