using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using NToastNotify.Helpers;
using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.DAL.Context;
using Project.ENTITY.Models;
using System.Net;
using System.Text.Json;
using WeatherForecast.Areas.Admin.IServices;
using WeatherForecast.Areas.Admin.Models;

namespace WeatherForecast.Areas.Admin.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly Weather_Info_Tab_Repository _witRep;

        public WeatherService(MyContext db)
        {
            _witRep = new Weather_Info_Tab_Repository(db);
        }

        public WeatherInfoVM GetActiveWeatherInfos()
        {
            WeatherInfoVM weatherInfo = new WeatherInfoVM();

            weatherInfo.weather_Info_Tabs = _witRep.GetActives().OrderByDescending(x => x.WeatherDate).ThenBy(x => x.CityName);

            return weatherInfo;
        }

        public WeatherInfoVM GetFilteredWeatherInfos(DateTime? WeatherDate, string? CityName, [FromServices] IToastNotification toast)
        {
            WeatherInfoVM weatherInfo = new WeatherInfoVM();

            if (WeatherDate.HasValue && WeatherDate != DateTime.MinValue)
            {
                weatherInfo.weather_Info_Tabs = _witRep.Where(x => x.WeatherDate == WeatherDate && x.Status != Project.ENTITY.Enums.DataStatus.Deleted).OrderByDescending(x => x.WeatherDate).ThenBy(x => x.CityName);

                if (!string.IsNullOrEmpty(CityName) && weatherInfo.weather_Info_Tabs != null)
                {
                    weatherInfo.weather_Info_Tabs.Where(x => x.CityName == CityName && x.Status != Project.ENTITY.Enums.DataStatus.Deleted).OrderByDescending(x => x.WeatherDate).ThenBy(x => x.CityName);
                }

                toast.AddSuccessToastMessage("Filtrelenmiş hava durumu başarı ile getirildi.", new ToastrOptions { Title = "Başarılı!" });

                return weatherInfo;
            }

            if (!string.IsNullOrEmpty(CityName))
            {
                weatherInfo.weather_Info_Tabs = _witRep.Where(x => x.CityName == CityName && x.Status != Project.ENTITY.Enums.DataStatus.Deleted).OrderByDescending(x => x.WeatherDate).ThenBy(x => x.CityName);

                toast.AddSuccessToastMessage("Filtrelenmiş hava durumu başarı ile getirildi.", new ToastrOptions { Title = "Başarılı!" });

                return weatherInfo;
            }

            weatherInfo.weather_Info_Tabs = _witRep.GetActives().OrderByDescending(x=>x.WeatherDate).ThenBy(x=>x.CityName);

            toast.AddErrorToastMessage("Filtrelenmiş hava durumu getirilemedi.", new ToastrOptions { Title = "Başarısız!" });

            return weatherInfo;
        }

        public async Task<WeatherInfoVM> AddWeatherDataAsync(WeatherInfoVM wiVM, [FromServices] IToastNotification toast)
        {
            string apiKey = "ff112ca932ff40a193c155645232411";

            string formattedDate = wiVM.weather_Info_Tab.WeatherDate.ToString("yyyy-MM-dd");

            string apiUrl;

            if (wiVM.weather_Info_Tab.WeatherDate <= DateTime.Now.Date)
            {
                apiUrl = $"https://api.weatherapi.com/v1/history.json?key={apiKey}&q={wiVM.weather_Info_Tab.CityName}&dt={formattedDate}&lang=tr";
            }
            else
            {
                apiUrl = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={wiVM.weather_Info_Tab.CityName}&dt={formattedDate}&lang=tr";
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    using (JsonDocument jsonDocument = JsonDocument.Parse(content))
                    {
                        var forecastDay = jsonDocument.RootElement
                            .GetProperty("forecast")
                            .GetProperty("forecastday")
                            .EnumerateArray()
                            .FirstOrDefault()
                            .GetProperty("day");

                        float avgTempC = forecastDay.GetProperty("avgtemp_c").GetSingle();
                        string conditionText = forecastDay.GetProperty("condition").GetProperty("text").GetString();
                        string conditionIcon = forecastDay.GetProperty("condition").GetProperty("icon").GetString();

                        wiVM.weather_Info_Tab.Temperature = avgTempC;
                        wiVM.weather_Info_Tab.MainStatus = conditionText;
                        wiVM.weather_Info_Tab.MainStatusIcon = conditionIcon;

                        _witRep.Add(wiVM.weather_Info_Tab);

                    }

                    toast.AddSuccessToastMessage("Hava durumu başarıyla eklendi", new ToastrOptions { Title = "Başarılı!" });

                    return wiVM;
                }
                else
                {
                    toast.AddErrorToastMessage($"Hava durumu eklenemedi, Hata Kodu: {new HttpRequestException($"Error: {response.StatusCode}")} ", new ToastrOptions { Title = "Başarısız!" });

                    return wiVM;
                }
            }
        }

        public WeatherInfoVM DeleteAsBatch(List<string> checkboxes, [FromServices] IToastNotification toast)
        {
            if (checkboxes != null && checkboxes.Any())
            {
                foreach (var id in checkboxes)
                {
                    Weather_Info_Tab witData = _witRep.Find(Convert.ToInt32(id));
                    _witRep.Delete(witData);
                }
                toast.AddErrorToastMessage("Hava durumları silindi", new ToastrOptions { Title = "Başarılı!" });

                return null;
            }
            else
            {
                toast.AddErrorToastMessage("Hava durumları silinemedi", new ToastrOptions { Title = "Başarısız!" });

                return null;
            }
        }

        public WeatherInfoVM GetDailyWeatherInfo(WeatherInfoVM wiVM)
        {
            wiVM.weather_Info_Tabs_Daily = _witRep.Where(x => x.WeatherDate == DateTime.Now.Date && x.CityName == wiVM.weather_Info_Tab.CityName && x.Status != Project.ENTITY.Enums.DataStatus.Deleted).FirstOrDefault();


            if (wiVM.weather_Info_Tabs_Daily != null)
            {
                switch (wiVM.weather_Info_Tabs_Daily.MainStatus)
                {
                    case string status when status.Contains("Güneşli"):
                        wiVM.WeatherBackgroundPath = "sunny.gif";
                        break;

                    case string status when status.Contains("Parçalı bulutlu"):
                        wiVM.WeatherBackgroundPath = "cloudy-sunny.gif";
                        break;

                    case string status when status.Contains("Bulutlu"):
                        wiVM.WeatherBackgroundPath = "cloudy.gif";
                        break;

                    case string status when status.Contains("Sis") || status.Contains("Pus"):
                        wiVM.WeatherBackgroundPath = "foggy.gif";
                        break;

                    case string status when status.Contains("Yağmur") || status.Contains("çisenti") || status.Contains("Yağış") || status.Contains("yağışlı") || status.Contains("yağmurlu"):
                        wiVM.WeatherBackgroundPath = "rainy.gif";
                        break;

                    case string status when status.Contains("Kar") || status.Contains("Tipi"):
                        wiVM.WeatherBackgroundPath = "snowy.gif";
                        break;

                    case string status when status.Contains("Gök Gürültülü"):
                        wiVM.WeatherBackgroundPath = "thunder.gif";
                        break;

                    default:

                        break;
                }

            }
            else
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
