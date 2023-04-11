using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Services;
using EIRA.Application.Services.API.JiraAPIV3;
using EIRA.Application.Statics;
using System.Security.Authentication;
using System.Text;
using static EIRA.Application.Statics.ExternalEndpoint;

namespace EIRA.Infrastructure.Services.API.JIraAPIV3
{
    public class AuthService : APIRequestBaseService, IAuthService
    {
        public AuthService(IHttpClientFactory httpClient, ICacheService cacheService) : base(httpClient, cacheService)
        {
        }

        public async Task<LoginResponse> Login(AuthLoginRequestBody authRequest)
        {
            try
            {
                //var auth = "cesar.figueroa@olsoftware.com:ATATT3xFfGF0UdrrdsheNGyG2EVDLZpiFCFa3wmxcGMxcC9cK7sU4L4o4ydmOGyFI0BVf5ClSWZgZTDY_ot6wFTGV11S8bCKS7b207VNbUVA6sB5UKF4fcSmi4Kf5CnozLLV6SpcqaCWISK53JSA-bpXSljZvhSFv9z0eDbcBG4_gORibwKJrJQ=7C437B5D";
                var authCredentials = $"{authRequest.UserName}:{authRequest.JiraApiKey}";
                var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(authCredentials));
                //this.SetTokenValue(token);

                var response = await this.SendAsync<LoginResponse>(new ApiRequest
                {
                    ApiType = ApiType.GET,
                    Url = $"{ExternalEndpoint.JiraAPIBaseV3}/myself",
                    StringRequest = $"{authRequest.UserName}:{authRequest.JiraApiKey}"

                }, authRequest: true);

                if (response is null || string.IsNullOrEmpty(response.AccountId))
                    throw new Exception(message: "Not Auth");

                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
