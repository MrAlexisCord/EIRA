using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class CustomFieldContext
    {
        [JsonProperty("type")]
        public string Type  { get; set; }
        [JsonProperty("contextId")]
        public string ContextId  { get; set; }
        [JsonProperty("optionId")]
        public string OptionId  { get; set; }
    }
}
