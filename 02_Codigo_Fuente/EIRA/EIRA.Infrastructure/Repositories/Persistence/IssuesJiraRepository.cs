using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Contracts.Persistence.Eira;
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
        private readonly ICustomFieldsCacheRepository _customFieldsCacheRepository;
        private readonly IIssueTypeCacheRepository _issueTypeCacheRepository;
        private readonly IMapper _mapper;

        public IssuesJiraRepository(IIssuesService issuesService, IResponsibleCacheRepository responsibleCacheRepository, IMapper mapper, ICustomFieldsCacheRepository customFieldsCacheRepository, IIssueTypeCacheRepository issueTypeCacheRepository)
        {
            _issuesService = issuesService;
            _responsibleCacheRepository = responsibleCacheRepository;
            _mapper = mapper;
            _customFieldsCacheRepository = customFieldsCacheRepository;
            _issueTypeCacheRepository = issueTypeCacheRepository;
        }

        public async Task<List<JiraUploadIssueErrorLog>> PostIssuesAsync(List<IssuesIncomingFile> source, RequestTypeTarget requestTypeTarget)
        {
            var logsError = new List<JiraUploadIssueErrorLog>();

            var responsibleList = await _responsibleCacheRepository.GetCachedResponsibleList();
            var issueTypeConfiguration = await _issueTypeCacheRepository.GetIssueTypeConfigurationFromCache();

            if (issueTypeConfiguration is null || !issueTypeConfiguration.Any())
            {
                throw new Exception(message: "No existe ninguna configuración de IssueTypes");
            }

            if (responsibleList is not null && responsibleList.Any())
            {
                var defaultResposibleId = await _responsibleCacheRepository.GetDefaultValue();
                var defaultResponsible = responsibleList.FirstOrDefault(x => x.Id == Convert.ToInt32(defaultResposibleId));

                foreach (var issue in source)
                {

                    if (string.IsNullOrEmpty(issue?.Proyecto?.Trim()))
                    {
                        logsError.Add(new JiraUploadIssueErrorLog
                        {
                            Proyecto = string.Empty,
                            ErrorMessage = "El campo Proyecto es obligatorio en todas las filas del archivo",
                            NumeroAranda = issue.NumeroCaso,
                            IssueKeyOrId = string.Empty,
                            Operation = CrudOperations.UPLOAD,
                        });
                    }
                    else
                    {
                        var fieldsOnLoad = await _customFieldsCacheRepository.GetFieldsOnLoadConfigurationByProjectKeyFromCache(issue.Proyecto);
                        if (fieldsOnLoad is null || !fieldsOnLoad.Any())
                        {
                            throw new Exception(message: $"No hay configuración para el proyecto con Clave '{issue?.Proyecto ?? string.Empty}'");
                        }

                        var body = issue.ToIssueCreateRequest(responsibleList, defaultResponsible, requestTypeTarget, issueTypeConfiguration);
                        var payload = body.ToDictionary(fieldsOnLoad); // Dynamic Payload

                        var comentarios = issue.Comentarios;
                        var issuesInJiraByAranda = await this.GetIssueByArandaAsync(issue.NumeroCaso, issue.Proyecto);

                        if (issuesInJiraByAranda is not null && issuesInJiraByAranda.Any())
                        {
                            var updateBody = _mapper.Map<IssueUpdateRequest>(body);
                            await UpdateProcess(updateBody, payload, issuesInJiraByAranda, logsError, comentarios);
                        }
                        else
                        {
                            await CreateProcess(body, payload, logsError, comentarios);
                        }
                    }
                }
            }
            return logsError;
        }

        public async Task<List<MinimalIssue>> GetIssueByArandaAsync(string aranda, string projectKeyOrId)
        {
            var response = await _issuesService.IssueByArandaNumber<BaseIssuesJiraResult<List<MinimalIssue>>>(aranda, projectKeyOrId);
            if (response is null || response.Total < 1 || response.Issues is null || !response.Issues.Any())
                return null;

            return response.Issues;
        }

        private async Task CreateProcess(IssueCreateRequest body, Dictionary<string, object> payload, List<JiraUploadIssueErrorLog> logsError, string comentarios = null)
        {
            try
            {
                var responseAs = await _issuesService.Create<IssueCreatedResponse, object>(new BaseFieldsPostBodyRequest<object> { Fields = payload }, "");
                await SetComments(responseAs.Key, comentarios);
            }
            catch (ExternalApiException ex)
            {
                string errMessage = ex.Result is not null && ex.Result.GetType()?.GetProperty("Message")?.GetValue(ex.Result) != null ? ex.Result.GetType()?.GetProperty("Message")?.GetValue(ex.Result).ToString() : ex.GetOneLineMessage();

                logsError.Add(new JiraUploadIssueErrorLog
                {
                    Proyecto = body?.Project?.Key ?? string.Empty,
                    ErrorMessage = errMessage,
                    NumeroAranda = body.NumeroAranda,
                    IssueKeyOrId = string.Empty,
                    Operation = CrudOperations.CREATE,
                });
            }
            catch (Exception ex)
            {

                logsError.Add(new JiraUploadIssueErrorLog
                {
                    Proyecto = body?.Project?.Key ?? string.Empty,
                    ErrorMessage = ex.Message,
                    NumeroAranda = body.NumeroAranda,
                    IssueKeyOrId = string.Empty,
                    Operation = CrudOperations.CREATE,
                });
            }
        }


        private async Task UpdateProcess(IssueUpdateRequest body, Dictionary<string, object> payload, List<MinimalIssue> issuesInJiraByAranda, List<JiraUploadIssueErrorLog> logsError, string comentarios = null)
        {

            if (issuesInJiraByAranda.Count > 1)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    Proyecto = body?.Project?.Key ?? string.Empty,
                    ErrorMessage = $"El Incidente No {body.NumeroAranda} se encuentra en varias issues",
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
                    var responseAs = await _issuesService.Update<IssueUpdatedResponse, object>(issueToModify.Key, new BaseFieldsPostBodyRequest<object> { Fields = payload }, "");
                    //await SetComments(issueToModify.Key, comentarios);
                }
                catch (ExternalApiException ex)
                {
                    string errMessage = ex.Result is not null && ex.Result.GetType()?.GetProperty("Message")?.GetValue(ex.Result) != null ? ex.Result.GetType()?.GetProperty("Message")?.GetValue(ex.Result).ToString() : ex.GetOneLineMessage();
                    logsError.Add(new JiraUploadIssueErrorLog
                    {
                        Proyecto = body?.Project?.Key ?? string.Empty,
                        ErrorMessage = errMessage,
                        NumeroAranda = body.NumeroAranda,
                        IssueKeyOrId = string.Join(",", issuesInJiraByAranda.Select(x => x.Key)),
                        Operation = CrudOperations.UPDATE,
                    });
                }
                catch (Exception ex)
                {

                    logsError.Add(new JiraUploadIssueErrorLog
                    {
                        Proyecto = body?.Project?.Key ?? string.Empty,
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

        public async Task<List<IssueConComentariosReport>> GetIssuesByProjectId(string projectId, List<string> statusIds)
        {
            var response = new List<Issue>();
            //string jqlStatement = "project%3DSE+AND+%22Fecha+Apertura%5BDate%5D%22+%3E+%222022-02-22%22+AND+%22Fecha+Apertura%5BDate%5D%22+%3E+%222023-01-10%22"
            var pageNumber = 0;
            var maxResults = 100;
            var statusIdsJql = string.Join(",", statusIds);
            var keepQueryingIssues = true;
            while (keepQueryingIssues)
            {
                var starAt = pageNumber * maxResults;
                var paginationString = $"&startAt={starAt}&maxResults={maxResults}";
                string jqlStatement = $"project=\"{projectId}\" AND status in ({statusIdsJql})";
                //string jqlStatement = $"project=SE AND \"Fecha Apertura[Date]\" >= \"{startDate:yyyy-MM-dd}\" AND \"Fecha Apertura[Date]\" <= \"{endDate:yyyy-MM-dd}\"";
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

            return response?.OrderBy(x => x.Prioridad)?.ThenBy(x => x.ResponsableCliente)?.ToList();

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
