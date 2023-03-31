using EIRA.API.Controllers.Common;
using EIRA.Application.Features.Issues.Commands.UploadIssues;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace EIRA.API.Controllers
{
    public class IssuesController : BaseController
    {

        [HttpPost("FilePost")]
        public async Task<IActionResult> FilePost(IFormFile issuesFiles)
        {


            using (MemoryStream stream = new MemoryStream())
            {
                await issuesFiles.CopyToAsync (stream);
                stream.Position = 0;

                var command = new UploadIssuesCommand() { FileStream = stream };

                return Ok(await Mediator.Send(command));
            }
        }
    }
}
