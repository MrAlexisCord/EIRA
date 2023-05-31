using AutoMapper;
using EIRA.Application.DTOs;
using EIRA.Domain.EIRAEntities.App;
using EIRA.Domain.EIRAEntities.Bas;

namespace EIRA.Application.Mappings.Profiles.Eira
{
    public class EiraProfile : Profile
    {
        public EiraProfile()
        {
            CreateMap<BasField, CustomFieldDto>().ReverseMap();
            CreateMap<AppConfigurationIssueType, IssueTypeConfigurationDTO>().ReverseMap();
        }
    }
}
