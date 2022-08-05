using AutoMapper;
using JwtHomework.Base;
using JwtHomework.Business;
using JwtHomework.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtHomework.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : CustomBaseController
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;


        public PeopleController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int page,[FromQuery] int limit, [FromQuery] string cacheKey)
        {
            //Pagination işlemi 
            List<Person> people = await _personService.GetPaginationAsync(page,limit,cacheKey);

            List<PersonListDto> peopleListDto = _mapper.Map<List<PersonListDto>>(people);

            return CreateActionResult(CustomResponseDto<List<PersonListDto>>.Success(200, peopleListDto,"İşlem başarılı"));
        }

   }
}
