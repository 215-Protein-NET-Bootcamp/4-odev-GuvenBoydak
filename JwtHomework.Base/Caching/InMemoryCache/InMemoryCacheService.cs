using JwtHomework.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JwtHomework.Base
{
    public class InMemoryCacheService<T>: IInMemoryCacheService<T> where T : BaseEntity
    {
        private readonly IMemoryCache _memoryCache;


        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public object Get(string cacheKey, object entity)
        {
            return _memoryCache.Get(cacheKey);
        }

        public void Remove(string cacheKey)
        {
           _memoryCache.Remove(cacheKey);
        }

        public void Set(string cacheKey, object entity, MemoryCacheEntryOptions options)
        {
            _memoryCache.Set(cacheKey, entity, options);
        }

        public bool TryGetValue(string cacheKey,out List<T> entity)
        {
            return _memoryCache.TryGetValue(cacheKey, out entity);
        }
    }
}
