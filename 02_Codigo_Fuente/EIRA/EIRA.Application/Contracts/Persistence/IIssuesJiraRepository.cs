using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IIssuesJiraRepository
    {
        Task<List<JiraUploadIssueErrorLog>> PostIssues(List<IssuesIncomingFile> source);
        Task<List<MinimalIssue>> GetIssueByAranda(string aranda);
    }
}
