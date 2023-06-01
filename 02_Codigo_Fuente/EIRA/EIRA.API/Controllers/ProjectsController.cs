using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Features.Projects.Queries.GetAllProjects;
using EIRA.Application.Models.External;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IAuthCacheRepository _cacheRepository;

        public ProjectsController(IAuthCacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        [HttpGet("GetAllProjects")]
        public async Task<IActionResult> GetAllProjects()
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            var response = await Mediator.Send(new GetAllProjectsQuery());
            return Ok(response);
        }
    }
}
