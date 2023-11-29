using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITY.Models
{
    public class Weather_Info_Tab : BaseEntity
    {
        public DateTime WeatherDate { get; set; }
        public string CityName { get; set; }
        public float? Temperature { get; set; }
        public string MainStatus { get; set; }
        public string MainStatusIcon { get; set; }
    }
}
