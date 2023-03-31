using EIRA.API.Controllers.Common;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class IssuesController: BaseController
    {

        [HttpPost("FilePost")]
        public async Task<IActionResult> FilePost(IFormFile issuesFiles)
        {

            return Ok(issuesFiles.FileName);
        }
    }
}
