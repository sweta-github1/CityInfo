using AutoMapper;
using CityInfo.API.Models.ResponseDto;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, CityWithoutPOI>();
            CreateMap<Entities.City, CityDto>();
        }
    }
}
