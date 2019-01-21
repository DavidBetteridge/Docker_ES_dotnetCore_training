using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DotNetCoreExample.Tests
{
    public class ColourTests : IClassFixture<WebApplicationFactory<DotNetCoreExample.Startup>>

    {
        private readonly WebApplicationFactory<DotNetCoreExample.Startup> _factory;

        public ColourTests(WebApplicationFactory<DotNetCoreExample.Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task Can_get_the_colours()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/colours/123");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            var text = await response.Content.ReadAsStringAsync();

            Assert.Equal("value", text);

        }
    }
}
