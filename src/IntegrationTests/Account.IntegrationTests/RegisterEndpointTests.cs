using Account.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using System.Net;
using Account.API.Models;
using Microsoft.Win32;

namespace Account.IntegrationTests
{
    public class RegisterEndpointTests : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public RegisterEndpointTests(TestWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task PostRegisterValidUser_ReturnNoContent()
        {
            var response = await _httpClient.PostAsJsonAsync("/register", new
            {
                Username = $"{Guid.NewGuid()}",
                Password = "123456"
            });

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PostRegisterInValidUser_ReturnBadRequest()
        {
            var response = await _httpClient.PostAsJsonAsync("/register", new
            {
                Username = "test@yourserver.com",
                Password = "123456"
            });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}