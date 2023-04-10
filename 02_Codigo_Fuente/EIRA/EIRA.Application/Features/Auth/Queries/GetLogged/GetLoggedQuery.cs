using MediatR;

namespace EIRA.Application.Features.Auth.Queries.GetLogged
{
    public class GetLoggedQuery : IRequest<object>
    {
        public string UserName { get; set; }
        public string JiraApiKey { get; set; }
    }
}
