using Bogus;
using Microsoft.EntityFrameworkCore;

namespace JwtHomework.DataAccess
{
    public class EfHomeworkDbContex:DbContext
    {
        public EfHomeworkDbContex(DbContextOptions options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var id = 1;
            var people = new Faker<Entities.Person>("tr")
                .RuleFor(x => x.Id, x => id++)
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.FirstName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth.ToUniversalTime())
                .RuleFor(x => x.Description, x => x.Person.Website)
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.CreatedDate, x => DateTime.UtcNow);

        }
        public DbSet<Entities.Person> People  { get; set; }
    }
}
