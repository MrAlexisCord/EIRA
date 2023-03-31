using EIRA.API.Controllers.Common;
using EIRA.Application.Features.IssueType.Queries.GetAllIssueTypes;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class IssueTypeController:BaseController
    {
        [HttpGet()]
        public async Task<IActionResult> GetIssueTypes()
        {
            return Ok(await Mediator.Send(new GetAllIssueTypesQuery()));
        }
    }
}
