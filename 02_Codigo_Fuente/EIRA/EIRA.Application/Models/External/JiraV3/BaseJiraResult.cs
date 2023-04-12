using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class BaseJiraResult<T> where T : class
    {
        [JsonProperty("self")]
        public string Self{ get; set; } = null;
        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }
        [JsonProperty("startAt")]
        public int StartAt { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("isLast")]
        public bool IsLast { get; set; }
        [JsonProperty("values")]
        public T Values { get; set; }
    }
}
