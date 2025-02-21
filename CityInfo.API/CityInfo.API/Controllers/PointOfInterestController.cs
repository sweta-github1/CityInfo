using Asp.Versioning;
using AutoMapper;
using CityInfo.API.Entities;
using CityInfo.API.Models.RequestDto;
using CityInfo.API.Models.ResponseDto;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/cities/{cityId}/pointsofinterest")]
    [ApiVersion(2)]
    public class PointOfInterestController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        public PointOfInterestController(
            IMapper mapper,
            ILogger<PointOfInterestController> logger,
            ICityInfoRepository cityInfoRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPointsOfInterest(int cityId)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }
            var pointsOfInterest = await _cityInfoRepository.GetPointsOfInterestForCityAsync(cityId);

            if (pointsOfInterest == null)
            {
                _logger.LogInformation($"Point of interest not found with for city id {cityId}");
                return NotFound();
            }
            return Ok(_mapper.Map<List<PointOfInterestDto>>(pointsOfInterest));
        }
        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<IActionResult> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }
            var pointOfInterest = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterest == null)
            {
                _logger.LogInformation($"Point of interest with id {pointOfInterestId} wasn't found for city with id {cityId}");
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePointOfInterest(int cityId, [FromBody] POICreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                return NotFound();
            }
            var pointOfInterestEntity = _mapper.Map<PointOfInterest>(pointOfInterest);

            pointOfInterestEntity.CityId = cityId;
            await _cityInfoRepository.AddPointOfInterestForCity(cityId, pointOfInterestEntity);

            if (!await _cityInfoRepository.SaveChangesAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            var createdPointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(pointOfInterestEntity);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new { cityId = cityId, pointOfInterestId = createdPointOfInterestToReturn.Id },
                createdPointOfInterestToReturn);
        }
        //update point of interest
        [HttpPut("{pointOfInterestId}")]
        public async Task<IActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, [FromBody] POIUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(pointOfInterest, pointOfInterestEntity);
            if (!await _cityInfoRepository.SaveChangesAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public async Task<IActionResult> PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<POIUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            var pointOfInterestToPatch = _mapper.Map<POIUpdateDto>(pointOfInterestEntity);

            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!await _cityInfoRepository.SaveChangesAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<IActionResult> DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var cityExists = await _cityInfoRepository.CityExistsAsync(cityId);
            if (!cityExists)
            {
                return NotFound();
            }
            var pointOfInterestEntity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            await _cityInfoRepository.DeletePointOfInterest(cityId, pointOfInterestEntity);
            if (!await _cityInfoRepository.SaveChangesAsync())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            return NoContent();


        }
    }
}
