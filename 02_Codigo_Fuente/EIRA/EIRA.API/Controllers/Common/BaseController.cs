using EIRA.Application.Statics.Misc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EIRA.API.Controllers.Common
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        protected IActionResult DownloadCSVFile(string filePath, string fileName)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception) { }

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            Response.Headers.Add("Content-Type", ContentTypes.PLAIN_TEXT);

            return File(fileBytes, ContentTypes.PLAIN_TEXT);
        }

        protected IActionResult DownloadExcelFile(string filePath, string fileName)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception) { }

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            Response.Headers.Add("Content-Type", ContentTypes.EXCEL);

            return File(fileBytes, ContentTypes.EXCEL);
        }
    }

}
