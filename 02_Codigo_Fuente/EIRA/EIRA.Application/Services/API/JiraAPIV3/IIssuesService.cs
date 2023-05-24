using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Services.API.JiraAPIV3
{
    public interface IIssuesService
    {
        Task<T> Create<T,Z>(BaseFieldsPostBodyRequest<Z> issueBody, string token);
        Task<T> Update<T,Z>(string issueIdOrKey, BaseFieldsPostBodyRequest<Z> issueBody, string token);
        Task<T> GetIssueByIdOrKey<T>(string idOrKey, string token);
        Task<T> IssueByArandaNumber<T>(string arandaNumber, string projectIdOrKey);
        Task<T> CommentOnIssue<T>(string idOrKey, CommentOnIssueCreateRequest commentBody);
        Task<T> GetCommentsByIssueIdOrKey<T>(string idOrKey);
        Task<T> GetIssuesByJQL<T>(string jqlStatement);
    }
}
