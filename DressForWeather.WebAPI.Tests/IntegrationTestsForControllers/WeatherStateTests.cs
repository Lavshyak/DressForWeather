using System.Net.Http.Json;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.Tests.Extensions;
using Xunit;

namespace DressForWeather.WebAPI.Tests.IntegrationTestsForControllers;

public class WeatherStateTests : TestFixtureWithPreLogin
{
	public WeatherStateTests(
		WebApplicationFactoryForTests factory) : base(factory)
	{
	}

	[Fact]
	public async Task AddingGettingWorks()
	{
		#region adding

		var t = await Client.AddRandomWeatherState();
		var input = t.Item1;
		var addedId = t.Item2;

		#endregion

		#region get previously added

		using var responseFromGet = await Client.GetAsync($"/Weather?id={addedId}");

		Assert.True(responseFromGet.IsSuccessStatusCode);

		var outputSearchResult =
			await responseFromGet.Content.ReadFromJsonAsync<OutputSearchResult<OutputWeatherState>>();

		Assert.NotNull(outputSearchResult);
		Assert.True(outputSearchResult.IsFound);

		Assert.Equal(addedId, outputSearchResult.Entity.Id);
		Assert.Equal(input.WindDirection, outputSearchResult.Entity.WindDirection);
		Assert.Equal(input.Humidity, outputSearchResult.Entity.Humidity);
		Assert.Equal(input.HowSunny, outputSearchResult.Entity.HowSunny);
		Assert.Equal(input.TemperatureCelsius, outputSearchResult.Entity.TemperatureCelsius);
		Assert.Equal(input.WindSpeedMps, outputSearchResult.Entity.WindSpeedMps);

		#endregion
	}
}