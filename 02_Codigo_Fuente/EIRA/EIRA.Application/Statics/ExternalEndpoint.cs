namespace EIRA.Application.Statics
{
    public static class ExternalEndpoint
    {
        public static string JiraAPIBaseV3 { get; set; }

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }
    }
}
