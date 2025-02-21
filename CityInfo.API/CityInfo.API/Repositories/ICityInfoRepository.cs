using CityInfo.API.Entities;

namespace CityInfo.API.Repositories
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistsAsync(int cityId);
        Task AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        Task DeletePointOfInterest(int cityId, PointOfInterest pointOfInterest);

        Task<bool> SaveChangesAsync();
    }
}
