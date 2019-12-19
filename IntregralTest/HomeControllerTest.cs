using ReadYourHeartOut;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;

namespace IntregrationTests
{
    public class HomeControllerTest : IClassFixture<WebApplicationFactory<Startup>>   
    {
        private WebApplicationFactory<Startup> _factory;

        public HomeControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        public async Task GetHttpRequests(string url)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
