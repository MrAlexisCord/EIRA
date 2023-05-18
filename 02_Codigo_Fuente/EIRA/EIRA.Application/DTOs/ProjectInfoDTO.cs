using Newtonsoft.Json;

namespace EIRA.Application.DTOs
{
    public class ProjectInfoDTO
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageURL { get; set; }
    }
}
