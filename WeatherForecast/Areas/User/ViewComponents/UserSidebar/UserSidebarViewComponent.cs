using Microsoft.AspNetCore.Mvc;

namespace WeatherForecast.Areas.User.ViewComponents.UserSidebar
{
    [ViewComponent]
    public class UserSidebarViewComponent : ViewComponent
    {
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
