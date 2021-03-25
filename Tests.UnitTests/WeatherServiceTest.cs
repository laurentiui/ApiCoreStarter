using Data.Domain.Entity;
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
        private IWeatherService _weatherService;

        [SetUp]
        public void Setup()
        {
            //var weatherRepository = new Mock<IWeatherRepository>();
            //_weatherService = new WeatherService(weatherRepository.Object);
            var weatherRepository = new WeatherRepositoryMock();
            _weatherService = new WeatherService(weatherRepository);
        }

        [Test]
        public void Test_GetForDateReturn_Cold()
        {
            Assert.AreEqual("cold", _weatherService.GetForDate(DateTime.Now));
        }

        [Test]
        public async Task Test_Insert_Returns10DegreesCold()
        {
            var result = await _weatherService.Insert(DateTime.Now, 10);

            Assert.AreEqual(10, result.TemperatureCelsius);
            Assert.AreEqual("cold", result.Summary);
        }
    }

    internal class WeatherRepositoryMock : IWeatherRepository
    {
        public async Task<Weather> Insert(Weather newEntity)
        {
            return newEntity;
        }
        public async Task<Weather> Update(Weather entity)
        {
            return entity;
        }

        public async Task<Weather> GetById(int playerId)
        {
            return null;
        }
        public async Task<IList<Weather>> ListAll()
        {
            return new List<Weather>();
        }
        public async Task Delete(Weather toRemove)
        {
        }
    }
}
