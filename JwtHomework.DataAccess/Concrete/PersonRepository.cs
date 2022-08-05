using Dapper;
using JwtHomework.Entities;
using System.Data;

namespace JwtHomework.DataAccess
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DapperHomeworkDbContext _db;

        public PersonRepository(DapperHomeworkDbContext db)
        {
            _db = db;
        }

        public async Task<List<Person>> GetPaginationAsync(int page, int limit)
        {
            using (IDbConnection con = _db.CreateConnection())
            {
                IEnumerable<Person> people = await con.QueryAsync<Person>("select * from \"People\" where \"Status\" != '2' order by \"Id\"  limit  @limit offset  @page",
                    new
                    {
                        limit = limit,
                        page = (page - 1) * limit
                    });
                return people.ToList();
            }
        }

    }
}
