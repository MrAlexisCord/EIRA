using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public partial class StatusesResponse
    {
        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("isLast")]
        public bool IsLast { get; set; }

        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("values")]
        public List<Status> Values { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }
    }

    public partial class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("statusCategory")]
        //public StatusCategory StatusCategory { get; set; }

        //[JsonProperty("scope")]
        //public Scope Scope { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        //[JsonProperty("usages")]
        //public List<object> Usages { get; set; }

        //[JsonProperty("workflowUsages")]
        //public List<object> WorkflowUsages { get; set; }
    }
}
