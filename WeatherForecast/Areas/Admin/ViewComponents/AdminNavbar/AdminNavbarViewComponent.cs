using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.ViewComponents.AdminNavbar
{
    [ViewComponent]
    public class AdminNavbarViewComponent : ViewComponent
    {
        IUserService _userService;

        public AdminNavbarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userId = ((ClaimsPrincipal)HttpContext.User).FindFirstValue("Id");

            UserVM uVM = _userService.GetUserInfos(Convert.ToInt32(userId));

            return View(uVM);
        }
    }
}
