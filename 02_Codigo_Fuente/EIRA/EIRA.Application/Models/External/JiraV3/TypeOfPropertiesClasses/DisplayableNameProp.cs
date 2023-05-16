using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public class DisplayableNameProp
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
