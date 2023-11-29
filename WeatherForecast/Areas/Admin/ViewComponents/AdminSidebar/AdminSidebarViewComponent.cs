using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.ENTITY.Models;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.ViewComponents.AdminSidebar
{
    [ViewComponent]
    public class AdminSidebarViewComponent : ViewComponent
    {
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
