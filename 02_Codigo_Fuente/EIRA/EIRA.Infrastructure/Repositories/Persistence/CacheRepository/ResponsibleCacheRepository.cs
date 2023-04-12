using EIRA.Application.Contracts.Persistence;
using EIRA.Application.Contracts.Persistence.CacheRepository;
using EIRA.Application.Models.External.JiraV3;
using EIRA.Application.Statics.CacheKeys;
using Microsoft.Extensions.Caching.Memory;

namespace EIRA.Infrastructure.Repositories.Persistence.CacheRepository
{
    public class ResponsibleCacheRepository: IResponsibleCacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IResponsibleJiraRepository _responsibleJiraRepository;

        public ResponsibleCacheRepository(IMemoryCache memoryCache, IResponsibleJiraRepository responsibleJiraRepository)
        {
            _memoryCache = memoryCache;
            _responsibleJiraRepository = responsibleJiraRepository;
        }

        public async Task<List<KeyValueList>> GetCachedResponsibleList()
        {
            return await _memoryCache.GetOrCreateAsync(ResponsibleKeys.RESPONSIBLES_KEY, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(7);
                return await _responsibleJiraRepository.GetResponsibleList();
            });
        }

        public async Task<string> GetDefaultValue()
        {
            return await _memoryCache.GetOrCreateAsync(ResponsibleKeys.RESPONSIBLE_DEFAULT_ID, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(7);
                return await _responsibleJiraRepository.GetDefaultValue();
            });
        }

    }
}
