using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class BaseFieldsPostBodyRequest<T>
    {
        [JsonProperty("fields")]
        public T Fields { get; set; }
    }
}
