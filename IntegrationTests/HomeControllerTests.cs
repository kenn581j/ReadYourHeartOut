using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<ReadYourHeartOut.Startup>>
    {
        private readonly WebApplicationFactory<ReadYourHeartOut.Startup> _factory;

        public HomeControllerTests(WebApplicationFactory<ReadYourHeartOut.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/About")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Home/Contact")]
        public async Task Get_HttpRequest(string url)
        {
            //den tester hver af de inlinedata der står foroven 
            //og tjekker om alle links virker


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
