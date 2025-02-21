using CityInfo.API.DBContext;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositories
{
    public class CityInfoRepository(CityInfoContext context) : ICityInfoRepository
    {
        private readonly CityInfoContext _context = context;

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(x=>x.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if(includePointsOfInterest)
            {
                return await _context.Cities.Include(x => x.PointsOfInterest).FirstOrDefaultAsync(x => x.Id == cityId);
            }
            return await _context.Cities.FirstOrDefaultAsync(x => x.Id == cityId);
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(x => x.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest.FirstOrDefaultAsync(x => x.CityId == cityId && x.Id == pointOfInterestId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(x => x.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city is not null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task DeletePointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

    }
}
