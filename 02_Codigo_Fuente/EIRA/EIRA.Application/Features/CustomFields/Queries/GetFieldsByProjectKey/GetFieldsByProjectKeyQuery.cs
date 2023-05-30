using EIRA.Application.Wrappers;
using MediatR;

namespace EIRA.Application.Features.CustomFields.Queries.GetFieldsByProjectKey
{
    public class GetFieldsByProjectKeyQuery: IRequest<Response<List<string>>>
    {
        public string ProjectKey { get; set; }
    }
}
