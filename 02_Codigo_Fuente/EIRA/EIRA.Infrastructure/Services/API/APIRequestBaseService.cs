using EIRA.Application.DTOs;
using EIRA.Application.Exceptions;
using EIRA.Application.Models.External;
using EIRA.Application.Models.External.JiraV3.Error;
using EIRA.Application.Services;
using EIRA.Application.Services.API;
using EIRA.Application.Statics;
using EIRA.Application.Statics.CacheKeys;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EIRA.Infrastructure.Services.API
{
    public class APIRequestBaseService : IAPIRequestBaseService
    {
        public ExternalResponseModel ResponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        private readonly ICacheService _cacheService;


        public APIRequestBaseService(IHttpClientFactory httpClient, ICacheService cacheService)
        {
            this.ResponseModel = new ExternalResponseModel();
            this.httpClient = httpClient;
            _cacheService = cacheService;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest, bool authRequest = false, bool expectJiraIssueError = false)
        {
            try
            {
                var client = httpClient.CreateClient("JiraAPI");
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                var credentials = string.Empty;
                if (!authRequest)
                {
                    var userInfo = _cacheService.GetByKey<UserInfoDTO>(AuthCacheKeys.USER_INFO);
                    if (userInfo is not null && !string.IsNullOrEmpty(userInfo.UserName) && !string.IsNullOrEmpty(userInfo.JiraAPIKey))
                    {
                        credentials = $"{userInfo.UserName}:{userInfo.JiraAPIKey}";
                    }
                }
                else
                {
                    credentials = apiRequest.StringRequest;
                }

                if (!string.IsNullOrEmpty(credentials))
                {
                    var encodedCredentials = Encoding.UTF8.GetBytes(credentials);
                    var token = Convert.ToBase64String(encodedCredentials);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
                }

                HttpResponseMessage apiResponse = null;

                message.Method = apiRequest.ApiType switch
                {
                    ExternalEndpoint.ApiType.POST => HttpMethod.Post,
                    ExternalEndpoint.ApiType.PUT => HttpMethod.Put,
                    ExternalEndpoint.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                if (expectJiraIssueError)
                {
                    FindOutJiraError(apiContent);
                }

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var externalApiException = new ExternalApiException(message: "Error on Jira Request")
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                    Result = ex
                };

                throw externalApiException;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        private void FindOutJiraError(string apiContent)
        {
            try
            {
                var apiErrorResponse = JsonConvert.DeserializeObject<JiraErrorResponse>(apiContent);
                if (apiErrorResponse is not null &&
                    ((apiErrorResponse.Errors is not null && apiErrorResponse.Errors.Any())
                    ||
                    (apiErrorResponse.ErrorMessages is not null && apiErrorResponse.ErrorMessages.Any()))
                    )
                {
                    if (apiErrorResponse.Errors is not null && apiErrorResponse.Errors.Any())
                    {
                        var errMessage = apiErrorResponse.Errors.Select(x => $"{x.Key} - {x.Value}");
                        throw new Exception(message: string.Join(" - ", errMessage));
                    }

                    if (apiErrorResponse.ErrorMessages is not null && apiErrorResponse.ErrorMessages.Any())
                    {
                        var errMessage = apiErrorResponse.ErrorMessages.Select(x => x.ToString());
                        throw new Exception(message: string.Join(" - ", errMessage));
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
