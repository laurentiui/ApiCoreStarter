using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _appDbContext;

        public WeatherRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Weather> Insert(DateTime date, int temperatureCelsius)
        {
            var newEntity = new Weather()
            {
                Day = date,
                TemperatureCelsius = temperatureCelsius
            };
            _appDbContext.Set<Weather>().Add(newEntity);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            _appDbContext.Entry(newEntity).State = EntityState.Detached;
            return newEntity;
        }
    }
}
