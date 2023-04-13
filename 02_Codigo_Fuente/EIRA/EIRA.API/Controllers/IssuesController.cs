using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Features.Issues.Commands.UploadIssues;
using EIRA.Application.Models.External;
using EIRA.Application.Models.LogModels;
using EIRA.Application.Services.Files;
using EIRA.Application.Statics.Misc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

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
        public async Task<IActionResult> FilePost(IFormFile issuesFiles)
        {
            if (issuesFiles is null)
            {
                return BadRequest("No such a file was selected");
            }

           

            using MemoryStream stream = new();
            await issuesFiles.CopyToAsync(stream);
            stream.Position = 0;

            var command = new UploadIssuesCommand() { FileStream = stream };
            var response = await Mediator.Send(command);
            if (response is not null && response.Any())
            {
                var fileName = $"log_errores-{DateTime.UtcNow:dd-MM-yyyy hh-mm-ss}.csv";
                var filePath = _csvService.WriteCSV(fileName, new string[]
                {
                    nameof(JiraUploadIssueErrorLog.NumeroAranda),
                    nameof(JiraUploadIssueErrorLog.IssueKeyOrId),
                    nameof(JiraUploadIssueErrorLog.ErrorMessage),
                    nameof(JiraUploadIssueErrorLog.Operation),
                }, response);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                var contentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName,
                };
                Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                Response.Headers.Add("Content-Type", ContentTypes.PLAIN_TEXT);

                return File(fileBytes, ContentTypes.PLAIN_TEXT);
            }
            return Ok();
        }
    }
}
