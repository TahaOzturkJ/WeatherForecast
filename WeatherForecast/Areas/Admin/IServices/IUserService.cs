using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.ENTITY.Models;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.IServices
{
    public interface IUserService
    {
        UserVM GetActiveUsers(UserVM uVM);

        UserVM GetFilteredUserInfos(string Name, string Email, string DefaultCityName, [FromServices] IToastNotification toast);

        UserVM GetUserInfos (int id);

        UserVM EditUserCity (int id, WeatherInfoVM wiVM, [FromServices] IToastNotification toast);

        UserVM EditUserProfile(int id, UserProfileVM upVM, [FromServices] IToastNotification toast);

        UserVM ChangeUserPassword(int id, UserSecurityVM usVM, [FromServices] IToastNotification toast);

        UserVM DeleteAsBatch(List<string> checkboxes, [FromServices] IToastNotification toast);

    }
}
