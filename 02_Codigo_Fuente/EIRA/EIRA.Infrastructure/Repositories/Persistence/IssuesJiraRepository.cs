using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class IssuesJiraRepository : IIssuesJiraRepository
    {
        private readonly IIssuesService _issuesService;

        public IssuesJiraRepository(IIssuesService issuesService)
        {
            _issuesService = issuesService;
        }

        public async Task<List<object>> PostIssues(List<IssuesIncomingFile> source)
        {
            foreach (var issue in source)
            {
                var body = issue.ToIssueCreateRequest();
                var response = await _issuesService.Create<object>(new BaseFieldsPostBodyRequest<IssueCreateRequest> { Fields = body }, "");
            }

            return null;
        }
    }
}
