using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    [Area("Admin")]
    public class LogController : Controller
    {
        IUserLogService _userLogService;

        public LogController(IUserLogService userLogService)
        {
            _userLogService = userLogService;
        }

        public IActionResult Index()
        {
            UserLogVM userLogVM = new UserLogVM();

            _userLogService.GetUserLogs(userLogVM);

            return View(userLogVM);
        }

        [HttpPost]
        public IActionResult Index(UserLogVM userLogVM, [FromServices] IToastNotification toast)
        {
            userLogVM = _userLogService.GetFilteredUserLogs(userLogVM.USER_LOG_TAB.USER_TAB.Name, userLogVM.USER_LOG_TAB.USER_TAB.Email, toast);

            return View(userLogVM);
        }
    }
}
