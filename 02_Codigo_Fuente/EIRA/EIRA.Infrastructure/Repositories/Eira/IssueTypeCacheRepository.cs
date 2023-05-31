using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Statics.CacheKeys;
using Microsoft.Extensions.Caching.Memory;

namespace EIRA.Infrastructure.Repositories.Eira
{
    public class IssueTypeCacheRepository : IIssueTypeCacheRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IIssueTypeRepository _repository;

        public IssueTypeCacheRepository(IMemoryCache memoryCache, IIssueTypeRepository repository)
        {
            _memoryCache = memoryCache;
            _repository = repository;
        }

        public async Task<List<IssueTypeConfigurationDTO>> GetIssueTypeConfigurationFromCache()
        {
            return await _memoryCache.GetOrCreateAsync(IssueTypeKeys.ISSUE_TYPE_CONFIGURATION, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(1);
                return await _repository.GetIssueTypeConfiguration();
            }); ;
        }
    }
}
