using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.ENTITY.Models;
using WeatherForecast.Areas.User.Models;

namespace WeatherForecast.Areas.User.IServices
{
    public interface IUserService
    {
        USER_TAB GetUserInfos (int id);

        USER_TAB EditUserCity (int id, WeatherInfoVM wiVM, [FromServices] IToastNotification toast);

        USER_TAB EditUserProfile(int id, UserProfileVM upVM, [FromServices] IToastNotification toast);

        USER_TAB ChangeUserPassword(int id, UserSecurityVM usVM, [FromServices] IToastNotification toast);
    }
}
