using EIRA.API.Controllers.Common;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsGlobalConfigurationByProjectKey;
using EIRA.Application.Features.Issues.Commands.UploadIssues;
using EIRA.Application.Features.Issues.Queries.GetReporteComentarios;
using EIRA.Application.Features.Projects.Queries.GetAllProjects;
using EIRA.Application.Models.Files.Outgoing;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    [Authorize]
    public class IssuesController : BaseController
    {
        private readonly IExcelService _excelService;

        public IssuesController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpPost("FilePost")]
        public async Task<IActionResult> FilePost(IFormFile formFile)
        {
            if (formFile is null)
                throw new NullFileException();

            using MemoryStream stream = new();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;

            var command = new UploadIssuesCommand() { FileStream = stream, FileName = formFile.FileName };

            var response = await Mediator.Send(command);
            if (response is not null && response.Any())
            {
                var fileName = $"log_errores-{DateTime.UtcNow.ExportableDateTimeFormat()}.xlsx";
                var propNames = new string[]
            {
                nameof(JiraUploadIssueErrorLog.Proyecto),
                nameof(JiraUploadIssueErrorLog.NumeroAranda),
                nameof(JiraUploadIssueErrorLog.IssueKeyOrId),
                nameof(JiraUploadIssueErrorLog.ErrorMessage),
                nameof(JiraUploadIssueErrorLog.Operation),
            };
                var filePath = _excelService.WriteExcel(response, propNames, fileName);
                return DownloadExcelFile(filePath, fileName);
            }

            return Ok();
        }

        [HttpPost("GetReporteSeguimiento")]
        public async Task<IActionResult> GetReporteSeguimiento(GetReporteComentariosQuery request)
        {
            var response = await Mediator.Send(request);
            var projectList = await Mediator.Send(new GetAllProjectsQuery());
            var projectSelected = projectList.Data.FirstOrDefault(x => x.Id == long.Parse(request.ProjectId ?? "0"));
            var responseHeadersInfo = await Mediator.Send(new GetFieldsFollowUpConfigurationByProjectKeyQuery { ProjectKey = projectSelected.Key });
            var propNames = responseHeadersInfo?.Data?.Select(x => x.FieldId)?.ToList()?.GetHeadersByFieldNames<IssueConComentariosReport>()?.ToArray();

            var fileName = $"reporte-seguimiento-{DateTime.Now.ExportableDateTimeFormat()}.xlsx";
            var filePath = _excelService.WriteExcel(response, propNames, fileName);

            return DownloadExcelFile(filePath, fileName);
        }

        [HttpPost("GetReporteTotal")]
        public async Task<IActionResult> GetReporteTotal(GetReporteComentariosQuery request)
        {
            var response = await Mediator.Send(request);

            var projectList = await Mediator.Send(new GetAllProjectsQuery());
            var projectSelected = projectList.Data.FirstOrDefault(x => x.Id == long.Parse(request.ProjectId ?? "0"));
            var responseHeadersInfo = await Mediator.Send(new GetFieldsGlobalConfigurationByProjectKeyQuery { ProjectKey = projectSelected.Key });
            var propNames = responseHeadersInfo?.Data?.Select(x => x.FieldId)?.ToList()?.GetHeadersByFieldNames<IssueConComentariosReport>()?.ToArray();

            var fileName = $"reporte-global-{DateTime.UtcNow.ExportableDateTimeFormat()}.xlsx";

            var filePath = _excelService.WriteExcel(response, propNames, fileName);
            return DownloadExcelFile(filePath, fileName);
        }

    }
}
