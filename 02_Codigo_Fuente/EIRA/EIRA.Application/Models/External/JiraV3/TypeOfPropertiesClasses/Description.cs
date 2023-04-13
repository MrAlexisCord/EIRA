using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public partial class Description
    {
        [JsonProperty("content")]
        public List<DescriptionContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }
}
