using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class IssueCreatedResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("self")]
        public string Self { get; set; }
    }
}
