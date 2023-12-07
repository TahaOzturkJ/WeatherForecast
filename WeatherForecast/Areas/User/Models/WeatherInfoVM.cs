using Project.ENTITY.Models;

namespace WeatherForecast.Areas.User.Models
{
    public class WeatherInfoVM
    {
        public string? WeatherBackgroundPath { get; set; }
        public Weather_Info_Tab weather_Info_Tab { get; set; }
        public IEnumerable<Weather_Info_Tab> weather_Info_Tabs { get; set; }
        public Weather_Info_Tab weather_Info_Tabs_Daily { get; set; }
        public IEnumerable<Weather_Info_Tab> weather_Info_Tabs_Weekly { get; set; }
    }
}
