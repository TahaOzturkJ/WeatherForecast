using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    [Area("Admin")]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            UserVM uVM = new UserVM();

            _userService.GetActiveUsers(uVM);

            return View(uVM);
        }

        [HttpPost]
        public IActionResult Index(UserVM uVM, [FromServices] IToastNotification toast)
        {
            uVM = _userService.GetFilteredUserInfos(uVM.USER_TAB.Name, uVM.USER_TAB.Email, uVM.USER_TAB.DefaultCityName, toast);

            return View(uVM);
        }

        public IActionResult AdminDeleteUserAsBatch(List<string> checkboxes, [FromServices] IToastNotification toast)
        {
            _userService.DeleteAsBatch(checkboxes, toast);

            return RedirectToAction("Index");
        }
    }
}
