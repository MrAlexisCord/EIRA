using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public partial class Customfield1010
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("content")]
        public List<Customfield10103_Content> Content { get; set; }
    }

    public partial class Customfield10103_Content
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("content")]
        public List<ContentContent> Content { get; set; }
    }
}
