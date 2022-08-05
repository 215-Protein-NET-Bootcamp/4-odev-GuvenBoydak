using AutoMapper;
using JwtHomework.Entities;

namespace JwtHomework.Business
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Person, PersonListDto>().ReverseMap();


        }
    }
}
