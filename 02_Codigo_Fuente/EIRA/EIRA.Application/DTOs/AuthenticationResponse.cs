using Newtonsoft.Json;

namespace EIRA.Application.DTOs
{
    public class AuthenticationResponse
    {
        [JsonProperty("token")]

        public string Token { get; set; }

        [JsonProperty("expirationDate")]
        public DateTime Expiration { get; set; }
    }
}
