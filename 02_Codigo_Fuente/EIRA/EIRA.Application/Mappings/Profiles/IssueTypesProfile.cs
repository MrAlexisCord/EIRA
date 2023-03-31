using AutoMapper;
using EIRA.Application.DTOs;
using EIRA.Domain.Entities;

namespace EIRA.Application.Mappings.Profiles
{
    public class IssueTypesProfile: Profile
    {
        public IssueTypesProfile()
        {

            #region DTOs
            CreateMap<IssueType, IssueTypeDto>().ReverseMap();
            #endregion

        }
    }
}