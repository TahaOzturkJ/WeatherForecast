using Project.ENTITY.Models;

namespace WeatherForecast.Areas.Admin.Models
{
    public class UserLogVM
    {
        public USER_LOG_TAB USER_LOG_TAB { get; set; }
        public IEnumerable<USER_LOG_TAB> USER_LOG_TABs { get; set; }
    }
}
