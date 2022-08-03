using JwtHomework.Entities;

namespace JwtHomework.Business
{
    public interface IPersonService:IService<Person>
    {
        Task<IEnumerable<Person>> GetByAccountIdAsync(int id);

        Task<List<Person>> GetPaginationAsync(int page, int limit,string cacheKey);
    }
}
