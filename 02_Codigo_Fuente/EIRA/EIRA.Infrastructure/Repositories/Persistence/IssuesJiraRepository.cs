using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class IssuesJiraRepository : IIssuesJiraRepository
    {
        private readonly IIssuesService _issuesService;
        private readonly IResponsibleCacheRepository _responsibleCacheRepository;

        public IssuesJiraRepository(IIssuesService issuesService, IResponsibleCacheRepository responsibleCacheRepository)
        {
            _issuesService = issuesService;
            _responsibleCacheRepository = responsibleCacheRepository;
        }

        public async Task<List<object>> PostIssues(List<IssuesIncomingFile> source)
        {
            var responsibleList = await _responsibleCacheRepository.GetCachedResponsibleList();
            if (responsibleList is not null && responsibleList.Any())
            {
                var defaultResposibleId = await _responsibleCacheRepository.GetDefaultValue();
                var defaultResponsible = responsibleList.FirstOrDefault(x => x.Id == Convert.ToInt32(defaultResposibleId));

                foreach (var issue in source)
                {
                    var body = issue.ToIssueCreateRequest(responsibleList, defaultResponsible);
                    var response = await _issuesService.Create<object>(new BaseFieldsPostBodyRequest<IssueCreateRequest> { Fields = body }, "");
                }
            }

            return null;
        }
    }
}
