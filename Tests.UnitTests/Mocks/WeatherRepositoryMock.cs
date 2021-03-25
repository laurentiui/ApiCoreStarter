using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
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
