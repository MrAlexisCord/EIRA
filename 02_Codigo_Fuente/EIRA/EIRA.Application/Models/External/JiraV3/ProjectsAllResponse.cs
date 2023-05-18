using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public partial class ProjectsAllResponse
    {
        [JsonProperty("expand")]
        public string Expand { get; set; }

        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("projectTypeKey")]
        public string ProjectTypeKey { get; set; }

        [JsonProperty("simplified")]
        public bool Simplified { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

    }

}
