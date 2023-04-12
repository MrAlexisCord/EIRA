using EIRA.Application.Models.External.JiraV3;

namespace EIRA.Application.Contracts.Persistence.CacheRepository
{
    public interface IResponsibleCacheRepository
    {
        Task<List<KeyValueList>> GetCachedResponsibleList();
        Task<string> GetDefaultValue();
    }
}
