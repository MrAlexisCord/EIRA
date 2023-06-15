using EIRA.Application.Models.External.JiraV3.TypeOfPropertiesClasses;
using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public partial class IssueCommentResponse
    {
        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        [JsonProperty("maxResults")]
        public long MaxResults { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }
    }

    public partial class Comment
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }

        [JsonProperty("updateAuthor")]
        public Author UpdateAuthor { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("updated")]
        public DateTime Updated { get; set; }

        //[JsonProperty("created")]
        //public string Created { get; set; }

        //[JsonProperty("updated")]
        //public string Updated { get; set; }

        [JsonProperty("jsdPublic")]
        public bool JsdPublic { get; set; }
    }

    public partial class Author
    {
        [JsonProperty("self")]
        public Uri Self { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

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

        [JsonProperty("accountType")]
        public string AccountType { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("content")]
        public List<BodyContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }

    public partial class BodyContent
    {
        [JsonProperty("content")]
        public List<ContentContent> Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    //public partial class ContentContent
    //{
    //    [JsonProperty("text")]
    //    public string Text { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }
    //}

}
