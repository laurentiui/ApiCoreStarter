using Data.Domain.Dto.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTests
{
    public class UserIntegrationTest
        : IClassFixture<CustomWebApplicationFactory<WebApi.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApi.Startup> _factory;

        public UserIntegrationTest(CustomWebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_NonRegisterdUserCanNotLogin()
        {
            string url = "/account/login";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var nonExistingUser = new UserLoginDto()
            {
                Email = $"{Guid.NewGuid()}@test.com",
                Password = "does-not-matter"
            };
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(nonExistingUser), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());


            //var text = await response.Content.ReadAsStringAsync();
            //var forecasts = JsonConvert.DeserializeObject<WeatherForecast[]>(text);

            //Assert.Equal(5, forecasts.Length);
            //Assert.Equal("cold", forecasts[0].Summary);
        }
    }
}
