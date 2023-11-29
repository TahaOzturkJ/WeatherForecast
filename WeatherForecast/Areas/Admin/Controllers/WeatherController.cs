using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    [Area("Admin")]
    public class WeatherController : Controller
    {
        IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            WeatherInfoVM wiVM = _weatherService.GetActiveWeatherInfos();

            return View(wiVM);
        }

        [HttpPost]
        public IActionResult Index(WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            wiVM = _weatherService.GetFilteredWeatherInfos(wiVM.weather_Info_Tab.WeatherDate, wiVM.weather_Info_Tab.CityName, toast);

            return View(wiVM);
        }

        public IActionResult AdminDeleteWeatherAsBatch(List<string> checkboxes, [FromServices] IToastNotification toast)
        {
            _weatherService.DeleteAsBatch(checkboxes, toast);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddWeatherData(WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            await _weatherService.AddWeatherDataAsync(wiVM, toast);

            return RedirectToAction("Index");
        }

    }
}
