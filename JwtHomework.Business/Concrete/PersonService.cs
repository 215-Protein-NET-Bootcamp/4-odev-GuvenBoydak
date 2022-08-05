
using JwtHomework.Base;
using JwtHomework.DataAccess;
using JwtHomework.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JwtHomework.Business
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IInMemoryCacheService<Person> _cacheService;



        public PersonService(IPersonRepository personRepository, IInMemoryCacheService<Person> cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }



        public async Task<List<Person>> GetPaginationAsync(int page, int limit,string cacheKey)
        {
            cacheKey = cacheKey + limit + page;

            //Cache data varmı kontrol ediyoruz ve out ile List<Person> tipinde people degişkeni yaratıyoruz.
            bool cachedData = _cacheService.TryGetValue(cacheKey, out List<Person> people);

            if (!cachedData)
            {
                //out parametresi ile yaratılan people a pagination yaptıgımız datayı cekiyoruz.
                 people = await _personRepository.GetPaginationAsync(page, limit);

                //optionsları bildiriyoruz.
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    //cachede kalıcak süreyi bildiriyoruz
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    //cacheden silinecek olan verilerin önceligini ve hangilerin kalıcı oldugunu bildiriyoruz.
                    Priority = CacheItemPriority.Normal
                };

                //cacheKey,people,options veriyoruz ve verileri cacheliyoruz.
                _cacheService.Set(cacheKey, people, options);
                return people;
            }         
            return people;
        }

    }
}
