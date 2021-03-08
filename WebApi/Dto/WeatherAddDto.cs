using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Dto
{
    public class WeatherAddDto
    {
        public DateTime Day { get; set; }
        public int TemperatureCelsius { get; set; }
    }
}
