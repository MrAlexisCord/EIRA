using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IIssuesService
    {
        Task<T> Create<T>(BaseFieldsPostBodyRequest<IssueCreateRequest> issueBody, string token);
        Task<T> GetIssueByIdOrKey<T>(string idOrKey, string token);
    }
}
