using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        public WeatherService()
        {

        }
        public string GetForDate(DateTime date)
        {
            return "cold";
        }
    }
}
