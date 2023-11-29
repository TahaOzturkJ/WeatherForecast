using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.ENTITY.Models;
using WeatherForecast.Areas.Auth.Models;
using NToastNotify;
using WeatherForecast.Areas.Auth.IServices;
using System.Security.Claims;

namespace WeatherForecast.Areas.Auth.Controllers
{
    [AllowAnonymous]
    [Area("Auth")]
    public class LoginController : Controller
    {
        IUserService _userService = null;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthModel uam, [FromServices] IToastNotification toast)
        {
            var user = await _userService.LoginAsync(uam,toast);

            if (user != null)
            {

                if (user.Role == Project.ENTITY.Enums.Role.Yonetici)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }

                return RedirectToAction("Index", "Dashboard", new { area = "User" });

            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult Register(UserAuthModel uam, [FromServices] IToastNotification toast)
        {
            if (ModelState.IsValid)
            {
                var registeredUser = _userService.Register(uam,toast);

                if (registeredUser != null)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("Index");

        }

        public IActionResult Logout([FromServices] IToastNotification toast)
        {
            string userId = User.FindFirstValue("Id");

            _userService.LogoutAsync(Convert.ToInt32(userId), toast);

            return RedirectToAction("Index");
        }
    }
}
