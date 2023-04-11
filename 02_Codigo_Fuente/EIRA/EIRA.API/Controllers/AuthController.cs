using EIRA.API.Controllers.Common;
using EIRA.Application.Features.Auth.Queries.GetLogged;
using EIRA.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public AuthController(IConfiguration configuration, ICacheService cacheService)
        {
            _configuration = configuration;
            _cacheService = cacheService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetLoggedQuery request)
        {
            var apiKey = _configuration.GetValue<string>("EIRAApiKey");
            request.SetApiKeyJwt(apiKey);
            return Ok(await Mediator.Send(request));
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _cacheService.ClearAllCachingMemory();
            return Ok();
        }
    }
}
