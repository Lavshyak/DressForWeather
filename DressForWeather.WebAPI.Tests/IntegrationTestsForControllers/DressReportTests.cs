using System.Net.Http.Json;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.Tests.Extensions;
using Xunit;

namespace DressForWeather.WebAPI.Tests.IntegrationTestsForControllers;

public class DressReportTests : TestFixtureWithPreLogin
{
	public DressReportTests(WebApplicationFactoryForTests factory) : base(factory)
	{
	}

	[Fact]
	public async Task AddingGettingWorks()
	{
		#region adding

		var t = await Client.AddRandomDressReport();
		var input = t.Item1;
		var addedId = t.Item2;

		#endregion

		#region get previously added

		using var responseFromGet = await Client.GetAsync($"/DressReport?id={addedId}");

		Assert.True(responseFromGet.IsSuccessStatusCode);

		var outputSearchResult =
			await responseFromGet.Content.ReadFromJsonAsync<OutputSearchResult<OutputDressReport>>();

		Assert.NotNull(outputSearchResult);
		Assert.True(outputSearchResult.IsFound);

		Assert.Equal(addedId, outputSearchResult.Entity.Id);
		Assert.True(input.ClothIds.All(id => outputSearchResult.Entity.ClothIds.Contains(id)));
		Assert.Equal(input.WeatherStateId, outputSearchResult.Entity.WeatherStateId);
		Assert.Equal(input.Feeling, outputSearchResult.Entity.Feeling);

		#endregion
	}
}