using Microsoft.Extensions.Caching.Memory;


namespace Infrastructure.CrossCuttingConcerns.Caching
{
    public class MemoryCacheManager : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add<T>(string key, T source)
        {
            // 60 dakika boyunca cache'de tutacak
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            };
            _memoryCache.Set(key, source, cacheEntryOptions);
        }

        public bool Contains(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public T Get<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T cachedData))
            {
                return cachedData;
            }
            return default; // Key bulunamazsa varsayılan değeri döndürün.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
