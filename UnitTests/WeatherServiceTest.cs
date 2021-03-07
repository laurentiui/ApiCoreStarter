using NUnit.Framework;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class WeatherServiceTest
    {
        private IWeatherService _weatherService;
        [SetUp]
        public void Setup()
        {
            _weatherService = new WeatherService();
        }

        [Test]
        public void Test_GetForDateReturn_Cold()
        {
            Assert.AreEqual("cold", _weatherService.GetForDate(DateTime.Now));
        }
    }
}
