using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public class IdentifiableProp
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
