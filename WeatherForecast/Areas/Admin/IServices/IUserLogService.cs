using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.IServices
{
    public interface IUserLogService
    {
        UserLogVM GetUserLogs(UserLogVM userLog);

        UserLogVM GetFilteredUserLogs(string Name, string Email, [FromServices] IToastNotification toast);

    }
}
