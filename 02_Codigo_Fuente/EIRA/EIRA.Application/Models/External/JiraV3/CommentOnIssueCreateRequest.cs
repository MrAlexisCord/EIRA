using Newtonsoft.Json;

namespace EIRA.Application.Models.External.JiraV3
{
    public class CommentOnIssueCreateRequest
    {
        [JsonProperty("body")]
        public CommentBody Body { get; set; }
    }

    public class CommentBody
    {
        [JsonProperty("content")]
        public List<ContentBody >Content { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }

    public class ContentBody
    {
        [JsonProperty("content")]
        public List<CommentText> Content { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    //public class CommentContent
    //{
    //    [JsonProperty("content")]
    //    public List<CommentText> Content { get; set; }
    //}


    public class CommentText
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
