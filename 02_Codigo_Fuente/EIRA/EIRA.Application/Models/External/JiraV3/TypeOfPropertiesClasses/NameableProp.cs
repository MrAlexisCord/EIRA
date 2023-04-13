using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public class NameableProp
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
