using EIRA.API.Controllers.Common;
using EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldFollowUpConfiguration;
using EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldGlobalConfiguration;
using EIRA.Application.Features.CustomFields.Commands.Create.CreateFieldOnLoadConfiguration;
using EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldFollowUpConfiguration;
using EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldGlobalConfiguration;
using EIRA.Application.Features.CustomFields.Commands.Delete.DeleteFieldOnLoadConfiguration;
using EIRA.Application.Features.CustomFields.Queries.GetAllowedCustomFields;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsFollowUpConfigurationByProjectKey;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsGlobalConfigurationByProjectKey;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsOnLoadConfigurationByProjectKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    [Authorize]
    public class CustomFieldsController : BaseController
    {

        [HttpGet("GetAllowedFields")]
        public async Task<IActionResult> GetAllowedFields()
        {
            var response = await Mediator.Send(new GetAllowedCustomFieldsQuery());
            return Ok(response);
        }

        [HttpGet("GetFieldsOnLoadByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsOnLoadByProjectKey(string projectKey)
        {
            var response = await Mediator.Send(new GetFieldsOnLoadConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpGet("GetFieldsFollowUpByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsFollowUpByProjectKey(string projectKey)
        {
            var response = await Mediator.Send(new GetFieldsFollowUpConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpGet("GetFieldsGlobalByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsGlobalByProjectKey(string projectKey)
        {
            var response = await Mediator.Send(new GetFieldsGlobalConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpPost("CreateFieldOnLoad")]
        public async Task<IActionResult> CreateFieldOnLoad(CreateFieldOnLoadConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateFieldFollowUpReport")]
        public async Task<IActionResult> CreateFieldFollowUpReport(CreateFieldFollowUpConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateFieldGlobalReport")]
        public async Task<IActionResult> CreateFieldGlobalReport(CreateFieldGlobalConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpPost("DeleteFieldOnLoad")]
        public async Task<IActionResult> DeleteFieldOnLoad(DeleteFieldOnloadConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("DeleteFieldFollowUpReport")]
        public async Task<IActionResult> DeleteFieldFollowUpReport(DeleteFieldFollowUpConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("DeleteFieldGlobalReport")]
        public async Task<IActionResult> DeleteFieldGlobalReport(DeleteFieldGlobalConfigurationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
