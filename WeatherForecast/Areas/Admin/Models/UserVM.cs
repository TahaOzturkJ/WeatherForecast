using Project.ENTITY.Models;

namespace WeatherForecast.Areas.Admin.Models
{
    public class UserVM
    {
        public USER_TAB USER_TAB { get; set; }

        public IEnumerable<USER_TAB> USER_TABs { get; set; }
    }
}
