using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Features.Issues.Commands.UploadIssues;
using EIRA.Application.Features.Issues.Queries.GetReporteComentarios;
using EIRA.Application.Models.External;
using EIRA.Application.Models.Files.Outgoing;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    //[Authorize]
    public class IssuesController : BaseController
    {
        private readonly IAuthCacheRepository _cacheRepository;
        private readonly ICSVService _csvService;
        private readonly IExcelService _excelService;

        public IssuesController(IAuthCacheRepository cacheRepository, ICSVService csvService, IExcelService excelService)
        {
            _cacheRepository = cacheRepository;
            _csvService = csvService;
            _excelService = excelService;
        }

        [HttpPost("FilePost")]
        public async Task<IActionResult> FilePost(IFormFile formFile)
        {
            if (formFile is null)
                throw new NullFileException();

            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "cesar.figueroa@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0A48NnkRMeLReao06WQBy93psM6hYoHvlaQomCCGZQKg9EQnw9N2aYVAm_B7OiIlZjPnhTn8IQuOsL9G7-lniBeCnGozMcQn4VinAF_rJmAz0v_tgjPgIIZba7EvH8Xo9zmSSHBaDQ3_KJqnTTmIa4fmGgTRcmZKnAxQ9r3vckEk=20FB03B8"
            });

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
                //nameof(IssueConComentariosReport.IssueKeyOrId),
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

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "cesar.figueroa@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0A48NnkRMeLReao06WQBy93psM6hYoHvlaQomCCGZQKg9EQnw9N2aYVAm_B7OiIlZjPnhTn8IQuOsL9G7-lniBeCnGozMcQn4VinAF_rJmAz0v_tgjPgIIZba7EvH8Xo9zmSSHBaDQ3_KJqnTTmIa4fmGgTRcmZKnAxQ9r3vckEk=20FB03B8"
            });

            var response = await Mediator.Send(new GetReporteComentariosQuery());
            var fileName = $"reporte-issues-{DateTime.Now.ExportableDateTimeFormat()}.xlsx";
            var propNames = new string[]
            {
                //nameof(IssueConComentariosReport.IssueKeyOrId),
                nameof(IssueConComentariosReport.NumeroAranda),
                nameof(IssueConComentariosReport.Entero),
                nameof(IssueConComentariosReport.EnteroNullable),
                nameof(IssueConComentariosReport.Fecha),
                nameof(IssueConComentariosReport.FechaNullable),
            };
            var filePath = _excelService.WriteExcel(response, propNames, fileName);
            return DownloadExcelFile(filePath, fileName);
        }

    }
}
