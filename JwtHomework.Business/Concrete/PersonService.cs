
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

        public async Task<List<Person>> GetActivesAsync()
        {
            return await _personRepository.GetActiveAsync();
        }

        public async Task<List<Person>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Person>> GetByAccountIdAsync(int id)
        {
            return await _personRepository.GetByAccountIdAsync(id);
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            Person person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                throw new InvalidOperationException($"{typeof(Person).Name}({id}) Not Found ");
            return person;
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

        public async Task InsertAsync(Person entity)
        {
            try
            {
                await _personRepository.AddAsync(entity);
            }
            catch (Exception)
            {

                throw new Exception($"Save_Error {typeof(Person).Name}");
            }
            
        }

        public async Task RemoveAsync(Person entity)
        {
            try
            {
                await _personRepository.DeleteAsync(entity);
            }
            catch (Exception)
            {

                throw new Exception($"Delete_Error {typeof(Person).Name}");
            }
            
        }

        public async Task UpdateAsync(Person entity)
        {
            try
            {
                await _personRepository.UpdateAsync(entity);
            }
            catch (Exception)
            {

                throw new Exception($"Update_Error {typeof(Person).Name}");
            }
            
        }
    }
}
