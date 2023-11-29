using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using WeatherForecast.Areas.User.IServices;
using WeatherForecast.Areas.User.IService;
using WeatherForecast.Areas.User.Models;
using Project.ENTITY.Models;

namespace WeatherForecast.Areas.User.Controllers
{
    [Authorize(Roles = "SonKullanici")]
    [Area("User")]
    public class DashboardController : Controller
    {
        IWeatherService _weatherService;
        IUserService _userService;

        public DashboardController(IWeatherService weatherService, IUserService userService)
        {
            _weatherService = weatherService;
            _userService = userService;
        }

        public async Task<IActionResult> Index([FromServices] IToastNotification toast)
        {
            WeatherInfoVM wiVM = new WeatherInfoVM();

            wiVM.weather_Info_Tab = new Weather_Info_Tab();

            string userId = User.FindFirstValue("Id");

            USER_TAB userInfo = _userService.GetUserInfos(Convert.ToInt32(userId));

            wiVM.weather_Info_Tab.CityName = userInfo.DefaultCityName;

            _weatherService.GetDailyWeatherInfo(wiVM);

            _weatherService.GetWeeklyWeatherInfo(wiVM);

            return View(wiVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditCity(WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            var userId = User.FindFirstValue("Id");

            _userService.EditUserCity(Convert.ToInt32(userId), wiVM, toast);

            return RedirectToAction("Index");
        }
    }
}
