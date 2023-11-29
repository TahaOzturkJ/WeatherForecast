using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.ENTITY.Models;
using WeatherForecast.Areas.Auth.Models;

namespace WeatherForecast.Areas.Auth.IServices
{
    public interface IUserService
    {
        USER_TAB Register(UserAuthModel uam, [FromServices] IToastNotification toast);
        Task<USER_TAB> LoginAsync(UserAuthModel uam, [FromServices] IToastNotification toast);
        Task<USER_TAB> LogoutAsync(int userId,[FromServices] IToastNotification toast);
        List<USER_TAB> GetAllUsers();
        USER_LOG_TAB CreateLog(USER_TAB user, Project.ENTITY.Enums.LogYon yon);
    }
}
