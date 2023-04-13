using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Exceptions;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics.Operations;

namespace EIRA.Infrastructure.Repositories.Persistence
{
    public class IssuesJiraRepository : IIssuesJiraRepository
    {
        private readonly IIssuesService _issuesService;
        private readonly IResponsibleCacheRepository _responsibleCacheRepository;
        private readonly IMapper _mapper;

        public IssuesJiraRepository(IIssuesService issuesService, IResponsibleCacheRepository responsibleCacheRepository, IMapper mapper)
        {
            _issuesService = issuesService;
            _responsibleCacheRepository = responsibleCacheRepository;
            _mapper = mapper;
        }

        public async Task<List<JiraUploadIssueErrorLog>> PostIssues(List<IssuesIncomingFile> source)
        {
            var logsError = new List<JiraUploadIssueErrorLog>();

            var responsibleList = await _responsibleCacheRepository.GetCachedResponsibleList();
            if (responsibleList is not null && responsibleList.Any())
            {
                var defaultResposibleId = await _responsibleCacheRepository.GetDefaultValue();
                var defaultResponsible = responsibleList.FirstOrDefault(x => x.Id == Convert.ToInt32(defaultResposibleId));

                foreach (var issue in source)
                {
                    var body = issue.ToIssueCreateRequest(responsibleList, defaultResponsible);
                    var issuesInJiraByAranda = await this.GetIssueByAranda(body.NumeroAranda);

                    if (issuesInJiraByAranda is not null && issuesInJiraByAranda.Any())
                    {
                        var updateBody = _mapper.Map<IssueUpdateRequest>(body);
                        await UpdateProcess(updateBody, issuesInJiraByAranda, logsError);
                    }
                    else
                    {
                        await CreateProcess(body, issuesInJiraByAranda, logsError);
                    }
                }
            }

            return logsError;
        }

        public async Task<List<MinimalIssue>> GetIssueByAranda(string aranda)
        {
            var response = await _issuesService.IssueByArandaNumber<BaseIssuesJiraResult<List<MinimalIssue>>>(aranda);
            if (response is null || response.Total < 1 || response.Issues is null || !response.Issues.Any())
                return null;

            return response.Issues;
        }

        private async Task CreateProcess(IssueCreateRequest body, List<MinimalIssue> issuesInJiraByAranda, List<JiraUploadIssueErrorLog> logsError)
        {
            try
            {
                //var response = await _issuesService.Create<object>(new BaseFieldsPostBodyRequest<IssueCreateRequest> { Fields = body }, "");
            }
            catch (ExternalApiException ex)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    ErrorMessage = ex.GetOneLineMessage(),
                    NumeroAranda = body.NumeroAranda,
                    IssueKeyOrId = string.Empty,
                    Operation = CrudOperations.CREATE,
                });
            }
            catch (Exception ex)
            {

                logsError.Add(new JiraUploadIssueErrorLog
                {
                    ErrorMessage = ex.Message,
                    NumeroAranda = body.NumeroAranda,
                    IssueKeyOrId = string.Empty,
                    Operation = CrudOperations.CREATE,
                });
            }
        }


        private async Task UpdateProcess(IssueUpdateRequest body, List<MinimalIssue> issuesInJiraByAranda, List<JiraUploadIssueErrorLog> logsError)
        {

            if (issuesInJiraByAranda.Count > 1)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    ErrorMessage = $"El Número de Aranda {body.NumeroAranda} se encuentra en varias issues [{string.Join(",", issuesInJiraByAranda.Select(x => x.Key))}]",
                    NumeroAranda = body.NumeroAranda,
                    IssueKeyOrId = string.Join(",", issuesInJiraByAranda.Select(x => x.Key)),
                    Operation = CrudOperations.UPDATE,
                });
            }
            else
            {
                try
                {
                    var issueToModify = issuesInJiraByAranda[0];
                    //var response = await _issuesService.Update<object>(issueToModify.Key, new BaseFieldsPostBodyRequest<IssueUpdateRequest> { Fields = body }, "");
                }
                catch (ExternalApiException ex)
                {
                    logsError.Add(new JiraUploadIssueErrorLog
                    {
                        ErrorMessage = ex.GetOneLineMessage(),
                        NumeroAranda = body.NumeroAranda,
                        IssueKeyOrId = string.Join(",", issuesInJiraByAranda.Select(x => x.Key)),
                        Operation = CrudOperations.UPDATE,
                    });
                }
                catch (Exception ex)
                {

                    logsError.Add(new JiraUploadIssueErrorLog
                    {
                        ErrorMessage = ex.Message,
                        NumeroAranda = body.NumeroAranda,
                        IssueKeyOrId = string.Join(",", issuesInJiraByAranda.Select(x => x.Key)),
                        Operation = CrudOperations.UPDATE,
                    });
                }
            }
        }
    }
}
