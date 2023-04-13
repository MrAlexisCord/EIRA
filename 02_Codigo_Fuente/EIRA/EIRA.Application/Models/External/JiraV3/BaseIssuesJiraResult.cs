using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class BaseIssuesJiraResult<T>
    {
        [JsonProperty("expand")]
        public string Expand { get; set; } = null;
        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }
        [JsonProperty("startAt")]
        public int StartAt { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("issues")]
        public T Issues{ get; set; }
    }
}
