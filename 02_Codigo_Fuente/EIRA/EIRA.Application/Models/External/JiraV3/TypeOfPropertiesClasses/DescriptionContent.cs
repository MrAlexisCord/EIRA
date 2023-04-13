using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public partial class DescriptionContent
    {
        [JsonProperty("content")]
        public List<ContentContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
