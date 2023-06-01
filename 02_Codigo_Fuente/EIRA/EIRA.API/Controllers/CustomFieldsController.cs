using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
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
using EIRA.Application.Models.External;
using Microsoft.AspNetCore.Mvc;

namespace EIRA.API.Controllers
{
    public class CustomFieldsController : BaseController
    {
        private readonly IAuthCacheRepository _cacheRepository;

        public CustomFieldsController(IAuthCacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        [HttpGet("GetAllowedFields")]
        public async Task<IActionResult> GetAllowedFields()
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            var response = await Mediator.Send(new GetAllowedCustomFieldsQuery());
            return Ok(response);
        }

        [HttpGet("GetFieldsOnLoadByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsOnLoadByProjectKey(string projectKey)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            var response = await Mediator.Send(new GetFieldsOnLoadConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpGet("GetFieldsFollowUpByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsFollowUpByProjectKey(string projectKey)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            var response = await Mediator.Send(new GetFieldsFollowUpConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpGet("GetFieldsGlobalByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsGlobalByProjectKey(string projectKey)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            var response = await Mediator.Send(new GetFieldsGlobalConfigurationByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }


        [HttpPost("CreateFieldOnLoad")]
        public async Task<IActionResult> CreateFieldOnLoad(CreateFieldOnLoadConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateFieldFollowUpReport")]
        public async Task<IActionResult> CreateFieldFollowUpReport(CreateFieldFollowUpConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CreateFieldGlobalReport")]
        public async Task<IActionResult> CreateFieldGlobalReport(CreateFieldGlobalConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            return Ok(await Mediator.Send(command));
        }


        [HttpPost("DeleteFieldOnLoad")]
        public async Task<IActionResult> DeleteFieldOnLoad(DeleteFieldOnloadConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            return Ok(await Mediator.Send(command));
        }

        [HttpPost("DeleteFieldFollowUpReport")]
        public async Task<IActionResult> DeleteFieldFollowUpReport(DeleteFieldFollowUpConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            return Ok(await Mediator.Send(command));
        }

        [HttpPost("DeleteFieldGlobalReport")]
        public async Task<IActionResult> DeleteFieldGlobalReport(DeleteFieldGlobalConfigurationCommand command)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "anay.valencia@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0YyowoeYenZ1DrzbJGBd5GTYbIgW01yHxTVyegauaSIttYPYj0UxYTnRYzYurXhenMz3CKSHTnMC2Zi1Gyh5Wn1QehRdD1n-DY0z8FkvNb5XEE5DJ3-mb9pbBzds-K7l6pd130ZWWtpj-X-agOSrFyg_lGCtDvGMkiBuGaSCrRZE=C77843FE"
            });

            return Ok(await Mediator.Send(command));
        }
    }
}
