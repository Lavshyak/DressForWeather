using System.Net.Http.Json;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.Tests.Extensions;
using Xunit;

namespace DressForWeather.WebAPI.Tests.IntegrationTestsForControllers;

public class ClothParameterPairTests : TestFixtureWithPreLogin
{
	public ClothParameterPairTests(
		WebApplicationFactoryForTests factory) : base(factory)
	{
	}

	[Fact]
	public async Task AddingGettingWorks()
	{
		#region adding

		var inputAndId = await Client.AddRandomPair();
		var input = inputAndId.Item1;
		var addedId = inputAndId.Item2;

		#endregion

		#region get previously added

		using var responseFromGet = await Client.GetAsync($"/ClotchParameterPair?id={addedId}");

		Assert.True(responseFromGet.IsSuccessStatusCode);

		var outputSearchResult =
			await responseFromGet.Content.ReadFromJsonAsync<OutputSearchResult<OutputClothParameterPair>>();

		Assert.NotNull(outputSearchResult);
		Assert.True(outputSearchResult.IsFound);

		Assert.Equal(addedId, outputSearchResult.Entity.Id);
		Assert.Equal(input.Key, outputSearchResult.Entity.Key);
		Assert.Equal(input.Value, outputSearchResult.Entity.Value);

		#endregion
	}
}