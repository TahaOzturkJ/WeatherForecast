using Microsoft.AspNetCore.Mvc;
using Project.ENTITY.Models;
using System.Security.Claims;
using WeatherForecast.Areas.User.Models;
using WeatherForecast.Areas.User.IServices;

namespace WeatherForecast.Areas.User.ViewComponents.UserNavbar
{
    [ViewComponent]
    public class UserNavbarViewComponent : ViewComponent
    {
        IUserService _userService;

        public UserNavbarViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userId = ((ClaimsPrincipal)HttpContext.User).FindFirstValue("Id");

            UserVM uVM = _userService.GetUserInfos(Convert.ToInt32(userId));

            return View(uVM);
        }
    }
}
