using EIRA.Application.DTOs;
using EIRA.Application.Models.External;
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

        public async Task<T> SendAsync<T>(ApiRequest apiRequest, bool authRequest = false)
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

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDto;


            }
            catch (Exception ex)
            {
                var dto = new ExternalResponseModel
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
