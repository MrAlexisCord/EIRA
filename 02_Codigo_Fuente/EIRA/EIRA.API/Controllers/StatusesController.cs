using EIRA.API.Controllers.Common;
using EIRA.Application.Features.Statuses.Queries.GetStatusesByProjectId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    [Authorize]
    public class StatusesController:BaseController
    {

        [HttpGet("GetStatusesByProjectId/{projectId}")]
        public async Task<IActionResult> GetStatusesByProjectId(string projectId)
       {
            var response = await Mediator.Send(new GetStatusesByProjectIdQuery { ProjectId = projectId});
            return Ok(response);
        }
    }
}
