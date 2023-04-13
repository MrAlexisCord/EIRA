using AutoMapper;
using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Mappings.Profiles.Jira
{
    public class IssuesProfile : Profile
    {
        public IssuesProfile()
        {
            CreateMap<IssueCreateRequest, IssueUpdateRequest>();
        }
    }
}
