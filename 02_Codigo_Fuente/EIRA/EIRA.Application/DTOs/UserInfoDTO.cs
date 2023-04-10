
namespace EIRA.Application.DTOs
{
    public class UserInfoDTO
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarURL { get; set; }
        public string JiraAPIKey { get; set; }
        public string AccountId { get; set; }
        public bool Active { get; set; }
    }
}
