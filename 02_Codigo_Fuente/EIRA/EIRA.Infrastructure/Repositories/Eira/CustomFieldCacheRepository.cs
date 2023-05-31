using EIRA.Application.Contracts.Persistence.Eira;
using EIRA.Application.DTOs;
using EIRA.Application.Statics.CacheKeys;
using Microsoft.Extensions.Caching.Memory;

namespace EIRA.Infrastructure.Repositories.Eira
{
    public class CustomFieldCacheRepository : ICustomFieldsCacheRepository
    {

        private readonly IMemoryCache _memoryCache;
        private readonly ICustomFieldsRepository _customFieldsRepository;

        public CustomFieldCacheRepository(IMemoryCache memoryCache, ICustomFieldsRepository customFieldsRepository)
        {
            _memoryCache = memoryCache;
            _customFieldsRepository = customFieldsRepository;
        }

        public async Task<List<CustomFieldDto>> GetAllowedFieldsFromCache()
        {
            return await _memoryCache.GetOrCreateAsync(CustomFieldsKeys.ALLOWED_FIELDS_KEY, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(1);
                return await _customFieldsRepository.GetAllowedFields();
            });
        }

        public async Task<List<string>> GetFieldsOnFollowUpReportByProjectKeyFromCache(string projectKey)
        {
            var key = $"{projectKey} - {CustomFieldsKeys.FOLLOW_UP_REPORT_CONFIGURATION}";
            return await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(1);
                return await _customFieldsRepository.GetFieldsOnFollowUpReportByProjectKey(projectKey);
            });
        }

        public async Task<List<string>> GetFieldsOnGlobalReportByProjectKeyFromCache(string projectKey)
        {
            var key = $"{projectKey} - {CustomFieldsKeys.FOLLOW_UP_REPORT_CONFIGURATION}";
            return await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(1);
                return await _customFieldsRepository.GetFieldsOnGlobalReportByProjectKey(projectKey);
            });
        }

        public async Task<List<string>> GetFieldsOnLoadConfigurationByProjectKeyFromCache(string projectKey)
        {
            var key = $"{projectKey} - {CustomFieldsKeys.FOLLOW_UP_REPORT_CONFIGURATION}";
            return await _memoryCache.GetOrCreateAsync(key, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(1);
                return await _customFieldsRepository.GetFieldsOnLoadConfigurationByProjectKey(projectKey);
            });
        }
    }
}
