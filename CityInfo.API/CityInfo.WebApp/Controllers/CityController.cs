using CityInfo.WebApp.ApiServices;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.WebApp.Controllers
{
    public class CityController : Controller
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityApiService _cityApiService;

        public CityController(ILogger<CityController> logger, ICityApiService cityApiService)
        {
            _logger = logger;
            _cityApiService = cityApiService;
        }
        public async Task<IActionResult> Index()
        {
            var cities = await _cityApiService.GetCities();
            return View(cities);
        }
    }
}
