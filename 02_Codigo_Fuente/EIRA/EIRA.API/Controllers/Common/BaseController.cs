using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers.Common
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
