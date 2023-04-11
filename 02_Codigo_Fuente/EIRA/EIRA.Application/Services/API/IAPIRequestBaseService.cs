using EIRA.Application.Models.External;

namespace EIRA.Application.Services.API
{
    public interface IAPIRequestBaseService: IDisposable
    {
        ExternalResponseModel ResponseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest, bool authRequest);
    }
}
