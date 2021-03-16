using Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class TravellerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TravellerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Post_EmptyList_ReturnsOk()
        {
            // Arrange
            var content = new StringContent("[]", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            response.EnsureSuccessStatusCode();
            var strResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<string>>(strResponse);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Post_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("invalid json]", Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_List_ReturnsOk()
        {
            // Arrange
            var list = new [] { "GOT-ARN", "LON-FRA", "HEL-GOT", "ARN-LON", "CPH-HEL" };
            var content = new StringContent(JsonSerializer.Serialize(list), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/", content);
            response.EnsureSuccessStatusCode();
            var strResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<string>>(strResponse);

            // Assert
            var expected = new[] { "CPH-HEL", "HEL-GOT", "GOT-ARN", "ARN-LON", "LON-FRA" };
            Assert.Equal(expected, result);
        }

        // Any additional tests..
    }
}
