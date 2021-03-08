using Data.Domain.Entity;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.IntegrationTests
{
    public class TestUtilities
    {
        public static void InitializeDbForTests(AppDbContext db)
        {
            //just for example - delete all weather info
            var allWeather = db.Set<Weather>().Select(w => w);
            db.Set<Weather>().RemoveRange(allWeather);

            db.SaveChanges();

        }
    }
}
