using Data.Repository;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
    public class WeatherServiceTest
    {
        //private IWeatherRepository _weatherRepository;
        private IWeatherService _weatherService;

        [SetUp]
        public void Setup()
        {
            var weatherRepository = new Mock<IWeatherRepository>();
            _weatherService = new WeatherService(weatherRepository.Object);
        }

        [Test]
        public void Test_GetForDateReturn_Cold()
        {
            Assert.AreEqual("cold", _weatherService.GetForDate(DateTime.Now));
        }
    }
}
