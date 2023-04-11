using EIRA.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace EIRA.Infrastructure.Services
{
    public class CacheService : ICacheService
    {

        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void ClearAllCachingMemory()
        {
            try
            {
                if (_memoryCache is not null && _memoryCache is MemoryCache memCache)
                {
                    memCache.Compact(1.0);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void ClearCachingMemoryByKey(string cachedKey)
        {
            try
            {
                if (_memoryCache is not null && _memoryCache is MemoryCache memCache
                    && !string.IsNullOrEmpty(cachedKey))
                {
                    if (memCache.Get(cachedKey) != null)
                        memCache.Remove(cachedKey);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void ClearCachingMemoryByKeys(string[] cachedKeys)
        {
            try
            {
                if (_memoryCache is not null && _memoryCache is MemoryCache memCache
                    && cachedKeys is not null && cachedKeys.Length > 0)
                {
                    foreach (var key in cachedKeys)
                    {
                        if (memCache.Get(key) != null)
                            memCache.Remove(key);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public T GetByKey<T>(string cachedKey) where T : class
        {
            try
            {
                if (_memoryCache is not null && _memoryCache is MemoryCache memCache
                    && !string.IsNullOrEmpty(cachedKey))
                {
                    var cacheObj = memCache.Get(cachedKey);
                    if (cacheObj is null) return null;

                    return (T)cacheObj;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
