using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Contracts.Persistence
{
    public interface IResponsibleJiraRepository
    {
        Task<List<KeyValueList>> GetResponsibleList();
        Task<string> GetDefaultValue();
    }
}
