using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace DressForWeather.WebAPI.Tests;

public class PrimeWebDefaultRequestShould
{
	private readonly HttpClient _client;

	public PrimeWebDefaultRequestShould()
	{
		// Arrange
		var server = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseEnvironment("Development"));
		_client = server.CreateClient();
	}

	[Fact]
	public async Task ReturnHelloWorld()
	{
		// Act
		var response = await _client.GetAsync("/");
		response.EnsureSuccessStatusCode();
		var responseString = await response.Content.ReadAsStringAsync();
		// Assert
		Assert.Equal("Hello World!", responseString);
	}
}