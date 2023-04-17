using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Exceptions;
using EIRA.Application.Extensions;
using EIRA.Application.Features.Issues.Commands.UploadIssues;
using EIRA.Application.Models.External;
using EIRA.Application.Services.Files;
using EIRA.Application.Statics.Reports;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    //[Authorize]
    public class IssuesController : BaseController
    {
        private readonly IAuthCacheRepository _cacheRepository;
        private readonly ICSVService _csvService;

        public IssuesController(IAuthCacheRepository cacheRepository, ICSVService csvService)
        {
            _cacheRepository = cacheRepository;
            _csvService = csvService;
        }

        [HttpPost("FilePost")]
        public async Task<IActionResult> FilePost(IFormFile formFile)
        {
            if (formFile is null)
                throw new NullFileException();

            using MemoryStream stream = new();
            await formFile.CopyToAsync(stream);
            stream.Position = 0;

            var command = new UploadIssuesCommand() { FileStream = stream };

                var response = await Mediator.Send(command);
                if (response is not null && response.Any())
                {
                    var fileName = $"log_errores-{DateTime.UtcNow.ExportableDateTimeFormat()}.csv";
                    var filePath = _csvService.WriteCSV(response, JiraHeadersCSV.ISSUES_ERROR_LOG_HEADERS, fileName);

                    return DownloadCSVFile(filePath, fileName);
                }

            return Ok();
        }

    }
}
