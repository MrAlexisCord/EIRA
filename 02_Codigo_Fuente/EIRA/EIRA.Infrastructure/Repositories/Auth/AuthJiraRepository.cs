using EIRA.Application.Contracts.Auth;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
using EIRA.Application.Services.API.JiraAPIV3;

namespace EIRA.Infrastructure.Repositories.Auth
{
    public class AuthJiraRepository : IAuthJiraRepository
    {
        private readonly IAuthService _authService;

        public AuthJiraRepository(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<UserInfoDTO> Login(AuthLoginRequestBody authRequest)
        {
            var response = await _authService.Login(authRequest) ?? throw new Exception(message: "No autorizado");
            
            var loginResponse = new UserInfoDTO
            {
                AccountId = response.AccountId,
                Active = response.Active,
                AvatarURL = response.AvatarUrls.The48X48.ToString(),
                DisplayName = response.DisplayName,
                JiraAPIKey = authRequest.JiraApiKey,
                UserName = authRequest.UserName,
            };

            return loginResponse;
        }
    }
}
