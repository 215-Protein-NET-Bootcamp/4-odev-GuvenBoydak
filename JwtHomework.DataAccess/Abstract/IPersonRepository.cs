using JwtHomework.Entities;
using System.Collections.Generic;

namespace JwtHomework.DataAccess
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPaginationAsync(int page, int limit);

    }
}
