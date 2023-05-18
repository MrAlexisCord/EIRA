using AutoMapper;
using EIRA.Application.DTOs;
using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Mappings.Profiles.Jira
{
    public class IssuesProfile : Profile
    {
        public IssuesProfile()
        {
            CreateMap<IssueCreateRequest, IssueUpdateRequest>();
            CreateMap<Status, StatusDTO>();
            CreateMap<ProjectsAllResponse, ProjectInfoDTO>()
                .ForMember(x => x.ImageURL, source => source.MapFrom(campo => campo != null && campo.AvatarUrls != null && campo.AvatarUrls.The48X48!=null ? campo.AvatarUrls.The48X48.ToString(): string.Empty));
        }
    }
}
