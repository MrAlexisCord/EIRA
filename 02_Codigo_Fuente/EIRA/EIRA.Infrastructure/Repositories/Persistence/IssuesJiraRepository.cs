using AutoMapper;
using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Exceptions;
using EIRA.Application.Mappings.Transforms;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Models.Files.Incoming;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics.Enumerations;
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
                        await UpdateProcess(updateBody, issuesInJiraByAranda, logsError);
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
                FieldCreateValidation(body, logsError);
                var response = await _issuesService.Create<IssueCreatedResponse>(new BaseFieldsPostBodyRequest<IssueCreateRequest> { Fields = body }, "");
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
                        await CommentOnIssue(response.Key, comentario);
                    }
                }
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

        private int FieldCreateValidation(IssueCreateRequest body, List<JiraUploadIssueErrorLog> logsError)
        {
            var errors = 0;
            var campos = new List<string>();
            if (string.IsNullOrEmpty(body.Frente?.Value.Trim()))
            {
                campos.Add("Frente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.Compania?.Value.Trim()))
            {
                campos.Add("Compañia");
                errors++;
            }

            if (string.IsNullOrEmpty(body.ResponsableCliente?.Value.Trim()) || body.ResponsableCliente?.Value == "Responsable no asignado")
            {
                campos.Add("Responsable Cliente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content.FirstOrDefault()?.Text?.Trim()))
            {
                campos.Add("Descripción Estado Cliente");
                errors++;
            }

            if (!body.FechaApertura.HasValue)
            {
                campos.Add("Fecha de apertura (registro)");
                errors++;
            }

            if (string.IsNullOrEmpty(body.NumeroAranda?.Trim()))
            {
                campos.Add("Número de caso");
                errors++;
            }

            if (errors > 0)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    NumeroAranda = body.NumeroAranda,
                    ErrorMessage = $"Los siguientes campos no deben estar vacíos: [{string.Join(",", campos)}]",
                    Operation = CrudOperations.CREATE
                });
            }


            return errors;

        }

        private async Task UpdateProcess(IssueUpdateRequest body, List<MinimalIssue> issuesInJiraByAranda, List<JiraUploadIssueErrorLog> logsError)
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
                    var errorsCount = FieldUpdateValidation(body, logsError);
                    var response = await _issuesService.Update<object>(issueToModify.Key, new BaseFieldsPostBodyRequest<IssueUpdateRequest> { Fields = body }, "");
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

        private int FieldUpdateValidation(IssueUpdateRequest body, List<JiraUploadIssueErrorLog> logsError)
        {
            var errors = 0;
            var campos = new List<string>();
            if (string.IsNullOrEmpty(body.Frente?.Value.Trim()))
            {
                campos.Add("Frente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.Compania?.Value.Trim()))
            {
                campos.Add("Compañía");
                errors++;
            }

            if (string.IsNullOrEmpty(body.ResponsableCliente?.Value.Trim()))
            {
                campos.Add("Responsable Cliente");
                errors++;
            }

            if (string.IsNullOrEmpty(body.DescripcionEstadoCliente?.Content?.FirstOrDefault()?.Content.FirstOrDefault()?.Text?.Trim()))
            {
                campos.Add("Descripción Estado Cliente");
                errors++;
            }

            if (!body.FechaApertura.HasValue)
            {
                campos.Add("Fecha de apertura (registro)");
                errors++;
            }

            if (string.IsNullOrEmpty(body.NumeroAranda?.Trim()))
            {
                campos.Add("Número de caso");
                errors++;
            }

            if (errors > 0)
            {
                logsError.Add(new JiraUploadIssueErrorLog
                {
                    NumeroAranda = body.NumeroAranda,
                    ErrorMessage = $"Los siguientes campos no deben estar vacíos {string.Join(",", campos)}",
                    Operation = CrudOperations.UPDATE
                });
            }

            return errors;
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
    }
}
