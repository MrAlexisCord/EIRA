using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class MinimalIssue
    {
        [JsonProperty("expand")]
        public string Expand { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("self")]
        public string Self { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
