using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class IssueCreateRequest
    {
        [JsonProperty("project")]
        public Assignee Project { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("issuetype")]
        public Assignee Issuetype { get; set; }

        [JsonProperty("parent")]
        public Assignee Parent { get; set; }

        [JsonProperty("assignee")]
        public Assignee Assignee { get; set; }

        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        [JsonProperty("customfield_10068")]
        public string Customfield10068 { get; set; }
    }

    public partial class Assignee
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Description
    {
        [JsonProperty("content")]
        public List<DescriptionContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }

    public partial class DescriptionContent
    {
        [JsonProperty("content")]
        public List<ContentContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class ContentContent
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Priority
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
