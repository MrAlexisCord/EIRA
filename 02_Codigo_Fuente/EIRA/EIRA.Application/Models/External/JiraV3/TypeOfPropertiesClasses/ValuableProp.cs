using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses
{
    public class ValuableProp
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
