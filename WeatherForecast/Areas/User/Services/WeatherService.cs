using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.DAL.Context;
using System.Text.Json;
using WeatherForecast.Areas.User.Models;
using WeatherForecast.Areas.User.IService;
using Project.ENTITY.Models;

namespace WeatherForecast.Areas.User.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly Weather_Info_Tab_Repository _witRep;

        public WeatherService(MyContext db)
        {
            _witRep = new Weather_Info_Tab_Repository(db);
        }

        public WeatherInfoVM GetDailyWeatherInfo(WeatherInfoVM wiVM)
        {
            wiVM.weather_Info_Tabs_Daily = _witRep.Where(x => x.WeatherDate == DateTime.Now.Date && x.CityName == wiVM.weather_Info_Tab.CityName && x.Status != Project.ENTITY.Enums.DataStatus.Deleted).FirstOrDefault();

            if (wiVM.weather_Info_Tabs_Daily == null)
            {
                return null;
            }

            return wiVM;
        }

        public WeatherInfoVM GetWeeklyWeatherInfo(WeatherInfoVM wiVM)
        {
            var weeklyData = _witRep
                .Where(x => x.WeatherDate >= DateTime.Now.Date && x.WeatherDate < DateTime.Now.AddDays(6) && x.CityName == wiVM.weather_Info_Tab.CityName && x.Status != Project.ENTITY.Enums.DataStatus.Deleted)
                .OrderBy(x => x.WeatherDate)
                .ToList();

            if (weeklyData == null || weeklyData.Count() == 0)
            {
                return null;
            }

            if (weeklyData.Count < 7)
            {
                DateTime currentDate = DateTime.Now.Date;

                for (int i = 0; i < weeklyData.Count - 1; i++)
                {
                    var currentDataDate = weeklyData[i].WeatherDate.Date;
                    var nextDataDate = weeklyData[i + 1].WeatherDate.Date;

                    if ((nextDataDate - currentDataDate).Days > 1)
                    {
                        var dummyDataDate = currentDataDate.AddDays(1);
                        var dummyData = new Weather_Info_Tab
                        {
                            WeatherDate = dummyDataDate,
                            Temperature = 0,
                        };

                        weeklyData.Insert(i + 1, dummyData);
                    }
                }

                var lastDataDate = weeklyData.Last().WeatherDate.Date;
                if ((currentDate.AddDays(6) - lastDataDate).Days > 0)
                {
                    var dummyDataDate = lastDataDate.AddDays(1);
                    var dummyData = new Weather_Info_Tab
                    {
                        WeatherDate = dummyDataDate,
                        Temperature = 0,
                    };

                    weeklyData.Add(dummyData);
                }
            }

            wiVM.weather_Info_Tabs_Weekly = weeklyData;

            return wiVM;
        }
    }
}
