using Asp.Versioning;
using AutoMapper;
using CityInfo.API.Models.ResponseDto;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [ApiVersion(1)]
    public class CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository = cityInfoRepository;
        private readonly IMapper _mapper = mapper;

        /// <summary>
        /// Gets all the cities
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the requested city</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityWithoutPOI>>> GetCities()
        {
            var cities = await _cityInfoRepository.GetCitiesAsync();
            var citiesWithoutPOI = _mapper.Map<IEnumerable<CityWithoutPOI>>(cities);

            //var citiesWithoutPOI = cities.Select(x => new CityWithoutPOI
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description
            //});
            return Ok(citiesWithoutPOI);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id, bool includePointsOfInterest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }
            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }
            return Ok(_mapper.Map<CityWithoutPOI>(city));
        }

    }
}
