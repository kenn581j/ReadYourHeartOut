using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests
{
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<ReadYourHeartOut.Startup>>
    {
        private readonly WebApplicationFactory<ReadYourHeartOut.Startup> _factory;

        public UsersControllerTests(WebApplicationFactory<ReadYourHeartOut.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Users/Create")]
        [InlineData("/Users/Delete")]
        [InlineData("/Users/Details")]
        [InlineData("/Users/Edit")]
        [InlineData("/Users/Index")]
        public async Task Get_HttpRequest(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
