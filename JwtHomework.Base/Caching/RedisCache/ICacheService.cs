using JwtHomework.Entities;

namespace JwtHomework.Base
{
    public interface ICacheService<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey,object entity,int expirationDay);

        Task RemoveAsync(string cacheKey);

    }
}
