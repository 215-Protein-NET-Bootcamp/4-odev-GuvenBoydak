using JwtHomework.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JwtHomework.Base
{
    public interface IInMemoryCacheService<T> where T : BaseEntity
    {  
            object Get(string cacheKey, object entity);

            void Set(string cacheKey, object entity, MemoryCacheEntryOptions options);

            void Remove(string cacheKey);

            bool TryGetValue(string cacheKey, out List<T> entity);   
    }
}
