using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.ENTITY.Models;
using WeatherForecast.Areas.User.Models;

namespace WeatherForecast.Areas.User.IServices
{
    public interface IUserService
    {
        UserVM GetUserInfos (int id);

        UserVM EditUserCity (int id, WeatherInfoVM wiVM, [FromServices] IToastNotification toast);

        UserVM EditUserProfile(int id, UserProfileVM upVM, [FromServices] IToastNotification toast);

        UserVM ChangeUserPassword(int id, UserSecurityVM usVM, [FromServices] IToastNotification toast);
    }
}
