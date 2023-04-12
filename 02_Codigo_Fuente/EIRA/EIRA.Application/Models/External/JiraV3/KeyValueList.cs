using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class KeyValueList
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
