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
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
