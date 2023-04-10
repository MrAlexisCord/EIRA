using EIRA.API.Controllers.Common;
using EIRA.Application.Features.Auth.Queries.GetLogged;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class AuthController:BaseController
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(GetLoggedQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

    }
}
