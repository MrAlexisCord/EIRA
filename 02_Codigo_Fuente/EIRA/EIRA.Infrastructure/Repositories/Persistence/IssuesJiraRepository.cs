using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Exceptions;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.Files.Outgoing;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics.Enumerations;
using EIRA.Application.Statics.Operations;
using EIRA.Application.Validators;
using System.Web;

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

        public async Task<List<JiraUploadIssueErrorLog>> PostIssuesAsync(List<IssuesIncomingFile> source, RequestTypeTarget requestTypeTarget)
        {
            var logsError = new List<JiraUploadIssueErrorLog>();

            var responsibleList = await _responsibleCacheRepository.GetCachedResponsibleList();
            if (responsibleList is not null && responsibleList.Any())
            {
                var defaultResposibleId = await _responsibleCacheRepository.GetDefaultValue();
                var defaultResponsible = responsibleList.FirstOrDefault(x => x.Id == Convert.ToInt32(defaultResposibleId));

                foreach (var issue in source)
                {
                    var body = issue.ToIssueCreateRequest(responsibleList, defaultResponsible, requestTypeTarget);
                    var comentarios = issue.Comentarios;
                    var issuesInJiraByAranda = await this.GetIssueByArandaAsync(body.NumeroAranda);

                    if (issuesInJiraByAranda is not null && issuesInJiraByAranda.Any())
                    {
                        var updateBody = _mapper.Map<IssueUpdateRequest>(body);
                        await UpdateProcess(updateBody, issuesInJiraByAranda, logsError, comentarios);
                    }
                    else
                    {
                        await CreateProcess(body, logsError, comentarios);
                    }
                }
            }

            return logsError;
        }

        public async Task<List<MinimalIssue>> GetIssueByArandaAsync(string aranda)
        {
            var response = await _issuesService.IssueByArandaNumber<BaseIssuesJiraResult<List<MinimalIssue>>>(aranda);
            if (response is null || response.Total < 1 || response.Issues is null || !response.Issues.Any())
                return null;

            return response.Issues;
        }

        private async Task CreateProcess(IssueCreateRequest body, List<JiraUploadIssueErrorLog> logsError, string comentarios = null)
        {
            try
            {
                body.FieldCreateValidation(logsError);
                var response = await _issuesService.Create<IssueCreatedResponse>(new BaseFieldsPostBodyRequest<IssueCreateRequest> { Fields = body }, "");
                await SetComments(response.Key, comentarios);
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

        private async Task UpdateProcess(IssueUpdateRequest body, List<MinimalIssue> issuesInJiraByAranda, List<JiraUploadIssueErrorLog> logsError, string comentarios = null)
        {

            if (issuesInJiraByAranda.Count > 1)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    ErrorMessage = $"El Número de Aranda {body.NumeroAranda} se encuentra en varias issues",
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
                    var errorsCount = body.FieldUpdateValidation(logsError);
                    var response = await _issuesService.Update<object>(issueToModify.Key, new BaseFieldsPostBodyRequest<IssueUpdateRequest> { Fields = body }, "");
                    await SetComments(issueToModify.Key, comentarios);
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

        public async Task<bool> CommentOnIssue(string idOrKey, string comment)
        {
            try
            {
                var commentBody = new CommentOnIssueCreateRequest
                {
                    Body = new CommentBody
                    {
                        Content = new List<ContentBody> {

                            new ContentBody
                            {
                                Content = new List<CommentText>
                                {
                                    new CommentText { Text = comment , Type="text"},
                                },
                                Type = "paragraph",
                            }
                        },
                        Type = "doc",
                        Version = 1,
                    }
                };
                var response = await _issuesService.CommentOnIssue<object>(idOrKey, commentBody);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task SetComments(string issueKey, string comentarios)
        {
            if (!string.IsNullOrEmpty(comentarios) && !string.IsNullOrEmpty(comentarios.Trim()))
            {
                var comentariosList = comentarios.Split("\n").Select(comentario =>
                {
                    var dateString = comentario?.Split(":")?[0];
                    var fechaComentario = DateTime.UtcNow;
                    if (dateString is not null)
                    {
                        var resultDate = DateTime.TryParse(dateString, out DateTime fechaComentarioNuevo);

                        if (resultDate)
                        {
                            fechaComentario = fechaComentarioNuevo;
                        }
                    }

                    return new { Fecha = fechaComentario, Comentario = comentario };
                }).OrderBy(x => x.Fecha).Select(x => x.Comentario);


                foreach (var comentario in comentariosList)
                {
                    await CommentOnIssue(issueKey, comentario);
                }
            }
        }

        public async Task<List<IssueConComentariosReport>> GetIssuesByFechaApertura(DateTime startDate, DateTime endDate)
        {
            var response = new List<Issue>();
            //string jqlStatement = "project%3DSE+AND+%22Fecha+Apertura%5BDate%5D%22+%3E+%222022-02-22%22+AND+%22Fecha+Apertura%5BDate%5D%22+%3E+%222023-01-10%22"
            var pageNumber = 0;
            var maxResults = 100;
            var keepQueryingIssues = true;
            while (keepQueryingIssues)
            {
                var starAt = pageNumber * maxResults;
                var paginationString = $"&startAt={starAt}&maxResults={maxResults}";
                string jqlStatement = $"project=SE AND \"Fecha Apertura[Date]\" >= \"{startDate:yyyy-MM-dd}\" AND \"Fecha Apertura[Date]\" <= \"{endDate:yyyy-MM-dd}\"";
                string encodedJqlStatement = $"{HttpUtility.UrlEncode(jqlStatement)}{paginationString}";
                var responseQuery = await _issuesService.GetIssuesByJQL<IssueWrapperResponse>(encodedJqlStatement);
                if (responseQuery != null && responseQuery.Issues != null && responseQuery.Issues.Any())
                {
                    response.AddRange(responseQuery.Issues);
                    pageNumber++;
                }
                else
                {
                    keepQueryingIssues = false;
                }
            }

            //GET COMMENTS

            return response.Any() ? await FromListIssueToListIssueConComentariosReport(response) : new List<IssueConComentariosReport>();
        }

        private async Task<List<IssueConComentariosReport>> FromListIssueToListIssueConComentariosReport(List<Issue> issues)
        {
            var response = new List<IssueConComentariosReport>();

            if (issues == null || !issues.Any()) return response;
            foreach (var issue in issues)
            {
                var issueCommentsFormatted = await GetIssueComments(issue.Key);
                var issueWithComments = IssueWrapperResponseTransform.FromIssueToIssueConComentariosReport(issue, issueCommentsFormatted);
                response.Add(issueWithComments);
            }
            return response;

        }

        private async Task<IEnumerable<string>> GetIssueComments(string idOrKey)
        {
            var issueComments = await _issuesService.GetCommentsByIssueIdOrKey<IssueCommentResponse>(idOrKey);
            var issueCommentsFormatted = issueComments?.Comments?.Select(x => x?.Body?.Content?.SelectMany(y => y?.Content)?.Select(z => z?.Text))?.SelectMany(t => t);
            issueCommentsFormatted = issueCommentsFormatted.Where(x => !string.IsNullOrEmpty(x));
            return issueCommentsFormatted;
        }
    }
}
