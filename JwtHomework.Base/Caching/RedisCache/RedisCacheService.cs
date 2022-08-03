using JwtHomework.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace JwtHomework.Base
{
    public class RedisCacheService<T>:ICacheService<T> where T : BaseEntity
    {
        private readonly IDistributedCache _disributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {

            _disributedCache = distributedCache;
        }

        public async Task<IEnumerable<T>> GetAsync(string cacheKey)
        {
            
           byte[] cachedData= await _disributedCache.GetAsync(cacheKey);

            string data=Encoding.UTF8.GetString(cachedData);

            List<T> values = JsonSerializer.Deserialize<List<T>>(data);

            return values.ToList();
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _disributedCache.RemoveAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object entity, int expirationDay)
        {
            string data =JsonSerializer.Serialize(entity);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddDays(expirationDay));
            await _disributedCache.SetAsync(cacheKey,bytes,options);
        }

    }
}
