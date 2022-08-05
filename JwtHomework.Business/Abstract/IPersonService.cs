using JwtHomework.Entities;

namespace JwtHomework.Business
{
    public interface IPersonService
    {
        Task<List<Person>> GetPaginationAsync(int page, int limit,string cacheKey);
    }
}
