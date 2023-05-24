using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.Error
{
    public class JiraErrorResponse
    {
        [JsonProperty("errorMessages")]
        public List<object> ErrorMessages { get; set; }

        [JsonProperty("errors")]
        public Dictionary<string, string> Errors { get; set; }
    }
}
