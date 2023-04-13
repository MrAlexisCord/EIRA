using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public partial class ContentContent
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
