using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NToastNotify;
using Project.ENTITY.Models;
using System.Security.Claims;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    [Area("Admin")]
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

                USER_TAB userInfo = _userService.GetUserInfos(Convert.ToInt32(userId)).USER_TAB;

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
