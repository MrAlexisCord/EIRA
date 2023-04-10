using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public partial class LoginResponse
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("groups")]
        public ApplicationRoles Groups { get; set; }

        [JsonProperty("applicationRoles")]
        public ApplicationRoles ApplicationRoles { get; set; }

        [JsonProperty("expand")]
        public string Expand { get; set; }
    }

    public partial class ApplicationRoles
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }

    public partial class AvatarUrls
    {
        [JsonProperty("48x48")]
        public Uri The48X48 { get; set; }

        [JsonProperty("24x24")]
        public Uri The24X24 { get; set; }

        [JsonProperty("16x16")]
        public Uri The16X16 { get; set; }

        [JsonProperty("32x32")]
        public Uri The32X32 { get; set; }
    }

}
