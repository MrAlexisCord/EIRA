using EIRA.API.Controllers.Common;
using EIRA.Application.Contracts.Auth.CacheRepository;
using EIRA.Application.Features.CustomFields.Queries.GetAllowedCustomFields;
using EIRA.Application.Features.CustomFields.Queries.GetFieldsByProjectKey;
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
                UserName = "cesar.figueroa@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0A48NnkRMeLReao06WQBy93psM6hYoHvlaQomCCGZQKg9EQnw9N2aYVAm_B7OiIlZjPnhTn8IQuOsL9G7-lniBeCnGozMcQn4VinAF_rJmAz0v_tgjPgIIZba7EvH8Xo9zmSSHBaDQ3_KJqnTTmIa4fmGgTRcmZKnAxQ9r3vckEk=20FB03B8"
            });

            var response = await Mediator.Send(new GetAllowedCustomFieldsQuery());
            return Ok(response);
        }

        [HttpGet("GetFieldsByProjectKey/{projectKey}")]
        public async Task<IActionResult> GetFieldsByProjectKey(string projectKey)
        {
            await _cacheRepository.GetUserInfoInCache(new AuthLoginRequestBody
            {
                UserName = "cesar.figueroa@olsoftware.com",
                JiraApiKey = "ATATT3xFfGF0A48NnkRMeLReao06WQBy93psM6hYoHvlaQomCCGZQKg9EQnw9N2aYVAm_B7OiIlZjPnhTn8IQuOsL9G7-lniBeCnGozMcQn4VinAF_rJmAz0v_tgjPgIIZba7EvH8Xo9zmSSHBaDQ3_KJqnTTmIa4fmGgTRcmZKnAxQ9r3vckEk=20FB03B8"
            });

            var response = await Mediator.Send(new GetFieldsByProjectKeyQuery { ProjectKey = projectKey });
            return Ok(response);
        }
    }
}
