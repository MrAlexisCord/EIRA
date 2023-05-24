using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public class KeyableProp
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
