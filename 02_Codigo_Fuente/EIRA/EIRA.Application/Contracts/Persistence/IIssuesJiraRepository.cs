using EIRA.Application.Models.Files.Incoming;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IIssuesJiraRepository
    {
        Task<List<object>> PostIssues(List<IssuesIncomingFile> source);
    }
}
