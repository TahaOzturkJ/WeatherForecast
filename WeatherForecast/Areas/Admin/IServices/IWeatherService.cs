using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.ENTITY.Models;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.IServices
{
    public interface IWeatherService
    {
        WeatherInfoVM GetActiveWeatherInfos();

        WeatherInfoVM GetFilteredWeatherInfos(DateTime? WeatherDate, string? CityName, [FromServices] IToastNotification toast);

        Task<WeatherInfoVM> AddWeatherDataAsync(WeatherInfoVM wiVM, [FromServices] IToastNotification toast);

        WeatherInfoVM DeleteAsBatch(List<string> checkboxes, [FromServices] IToastNotification toast);

        WeatherInfoVM GetDailyWeatherInfo(WeatherInfoVM wiVM);

        WeatherInfoVM GetWeeklyWeatherInfo(WeatherInfoVM wiVM);
    }
}
