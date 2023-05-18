using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.Files.Outgoing;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Statics.Enumerations;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IIssuesJiraRepository
    {
        Task<List<JiraUploadIssueErrorLog>> PostIssuesAsync(List<IssuesIncomingFile> source, RequestTypeTarget requestTypeTarget);
        Task<List<MinimalIssue>> GetIssueByArandaAsync(string aranda);
        Task<bool> CommentOnIssue(string idOrKey, string comment);
        Task<List<IssueConComentariosReport>> GetIssuesByProjectId(string projectId, List<string> statusIds);
    }
}
