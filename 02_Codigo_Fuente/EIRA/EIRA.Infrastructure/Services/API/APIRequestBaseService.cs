using EIRA.Application.Models.External;
using EIRA.Application.Services.API;
using EIRA.Application.Statics;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EIRA.Infrastructure.Services.API
{
    public class APIRequestBaseService: IAPIRequestBaseService
    {
        public ExternalResponseModel ResponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public APIRequestBaseService(IHttpClientFactory httpClient)
        {
            this.ResponseModel = new ExternalResponseModel();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("JiraAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                //apiRequest.AccessToken = "Basic Y2VzYXIuZmlndWVyb2FAb2xzb2Z0d2FyZS5jb206QVRBVFQzeEZmR0YwQTQ4Tm5rUk1lTFJlYW8wNldRQnk5M3BzTTZoWW9IdmxhUW9tQ0NHWlFLZzlFUW53OU4yYVlWQW1fQjdPaUlsWmpQbmhUbjhJUXVPc0w5RzctbG5pQmVDbkdvek1jUW40VmluQUZfckptQXowdl90Z2pQZ0lJWmJhN0V2SDhYbzl6bVNTSEJhRFEzX0tKcW5UVG1JYTRmbUdnVFJjbVpLbkF4UTlyM3Zja0VrPTIwRkIwM0I4";
                apiRequest.AccessToken = "Y2VzYXIuZmlndWVyb2FAb2xzb2Z0d2FyZS5jb206QVRBVFQzeEZmR0YwQTQ4Tm5rUk1lTFJlYW8wNldRQnk5M3BzTTZoWW9IdmxhUW9tQ0NHWlFLZzlFUW53OU4yYVlWQW1fQjdPaUlsWmpQbmhUbjhJUXVPc0w5RzctbG5pQmVDbkdvek1jUW40VmluQUZfckptQXowdl90Z2pQZ0lJWmJhN0V2SDhYbzl6bVNTSEJhRFEzX0tKcW5UVG1JYTRmbUdnVFJjbVpLbkF4UTlyM3Zja0VrPTIwRkIwM0I4";

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", apiRequest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;

                switch (apiRequest.ApiType)
                {
                    case ExternalEndpoint.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ExternalEndpoint.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ExternalEndpoint.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

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
