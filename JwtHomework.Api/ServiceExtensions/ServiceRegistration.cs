using AutoMapper;
using JwtHomework.Base;
using JwtHomework.Business;
using JwtHomework.DataAccess;

namespace JwtHomework.Api
{
    public static class ServiceRegistration
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddSingleton<DapperHomeworkDbContext>();
            services.AddScoped<IPersonRepository,PersonRepository>();
            services.AddScoped<IPersonService,PersonService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            // memoryCashe
            services.AddMemoryCache();

            services.AddScoped(typeof(IInMemoryCacheService<>), typeof(InMemoryCacheService<>));
        }

    }
}
