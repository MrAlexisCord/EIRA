namespace EIRA.Application.Services
{
    public interface ICacheService
    {

        T GetByKey<T>(string cachedKey) where T : class;
        void ClearAllCachingMemory();
        void ClearCachingMemoryByKeys(string[] cachedKeys);
        void ClearCachingMemoryByKey(string cachedKey);
    }
}
