using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    [Area("Admin")]
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue("Id");

            UserProfileVM user = new UserProfileVM();
            user.USER_TAB = _userService.GetUserInfos(Convert.ToInt32(userId)).USER_TAB;

            user.Name = user.USER_TAB.Name;
            user.Email = user.USER_TAB.Email;
            user.DefaultCityName = user.USER_TAB.DefaultCityName;

            return View(user);
        }

        [HttpPost]
        public IActionResult EditProfile(UserProfileVM user, [FromServices] IToastNotification toast)
        {
            if (!ModelState.IsValid)
            {
                toast.AddErrorToastMessage("Lütfen bütün alanları doldurun.", new ToastrOptions { Title = "Başarısız!" });

                return RedirectToAction("Index");
            }

            string userId = User.FindFirstValue("Id");

            _userService.EditUserProfile(Convert.ToInt32(userId), user, toast);

            return RedirectToAction("Index");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(UserSecurityVM usVM, [FromServices] IToastNotification toast)
        {
            if (!ModelState.IsValid)
            {
                toast.AddErrorToastMessage("Lütfen bütün alanları doldurun.", new ToastrOptions { Title = "Başarısız!" });

                return RedirectToAction("ChangePassword", usVM);
            }

            string userId = User.FindFirstValue("Id");

            _userService.ChangeUserPassword(Convert.ToInt32(userId), usVM, toast);

            return RedirectToAction("Index");
        }
    }
}
