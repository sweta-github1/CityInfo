using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.RequestDto;
using CityInfo.API.Models.ResponseDto;

namespace CityInfo.API.Profiles
{
    public class POIProfile : Profile
    {
        public POIProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<POICreationDto, PointOfInterest>();
            CreateMap<POIUpdateDto, PointOfInterest>();
            CreateMap<PointOfInterest, POIUpdateDto>();

        }
    }
}
