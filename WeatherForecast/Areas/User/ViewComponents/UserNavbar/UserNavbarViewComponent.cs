using Microsoft.AspNetCore.Mvc;
using Project.ENTITY.Models;
using System.Security.Claims;
using WeatherForecast.Areas.User.IServices;

namespace WeatherForecast.Areas.User.ViewComponents.UserNavbar
{
    [ViewComponent]
    public class UserNavbarViewComponent : ViewComponent
    {

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
