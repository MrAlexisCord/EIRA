using EIRA.API.Controllers.Common;
using EIRA.Application.Features.Projects.Queries.GetAllProjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    [Authorize]
    public class ProjectsController : BaseController
    {
        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            var response = await Mediator.Send(new GetAllProjectsQuery());
            return Ok(response);
        }
    }
}
