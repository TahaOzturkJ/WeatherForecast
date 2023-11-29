using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using WeatherForecast.Areas.User.Models;

namespace WeatherForecast.Areas.User.IService
{
    public interface IWeatherService
    {
        WeatherInfoVM GetDailyWeatherInfo(WeatherInfoVM wiVM);

        WeatherInfoVM GetWeeklyWeatherInfo(WeatherInfoVM wiVM);
    }
}
