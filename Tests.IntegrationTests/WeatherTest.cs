using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace Tests.IntegrationTests
{
    public class WeatherTest
        : IClassFixture<WebApplicationFactory<WebApi.Startup>>
    {
        private readonly WebApplicationFactory<WebApi.Startup> _factory;

        public WeatherTest(WebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Endpoint_TestByLaur()
        {
            string url = "/weatherforecast";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());


            var text = await response.Content.ReadAsStringAsync();
            var forecasts = JsonConvert.DeserializeObject<WeatherForecast[]>(text);

            Assert.Equal(5, forecasts.Length);
            Assert.Equal("cold", forecasts[0].Summary);
        }
    }
}
