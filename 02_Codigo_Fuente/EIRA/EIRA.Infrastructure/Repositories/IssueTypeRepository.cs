using EIRA.Application.Contracts.Persistence.IssueType;
using EIRA.Domain.Entities;

namespace EIRA.Infrastructure.Repositories
{
    public class IssueTypeRepository : IIssueTypeRepository
    {
        public async Task<List<IssueType>> GetAll()
        {
            var resonseMoq = new List<IssueType>
            {
                new IssueType { Id = 1, AvatarId = 1,Description = "Desc Moq", HierarchyLevel=0, IconUrl=new Uri("https://www.google.com"), Name="Moq 1",Self=new Uri("https://www.jiramoq.com/1"), Subtask= false, UntranslatedName="MoqTranslated 1" },
                new IssueType { Id = 2, AvatarId = 2,Description = "Desc Moq", HierarchyLevel=0, IconUrl=new Uri("https://www.google.com"), Name="Moq 2",Self=new Uri("https://www.jiramoq.com/2"), Subtask= false, UntranslatedName="MoqTranslated 2" },
            };

            return resonseMoq;
        }
    }
}
