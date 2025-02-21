using CityInfo.API.Entities;
using Newtonsoft.Json;

namespace CityInfo.WebApp.ApiServices
{
    public interface ICityApiService
    {
        public Task<IEnumerable<City>> GetCities();
        public Task<City> GetCity(int id);
    }
    public class CityApiService : ICityApiService
    {
        private readonly HttpClient _httpClient;
        public CityApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            var response = await _httpClient.GetAsync("api/cities");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var cities = JsonConvert.DeserializeObject<IEnumerable<City>>(content);
            return cities;
        }
        public async Task<City> GetCity(int id)
        {
            var response = await _httpClient.GetAsync($"api/cities/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<City>(content);
            return city;
        }
    }
}
