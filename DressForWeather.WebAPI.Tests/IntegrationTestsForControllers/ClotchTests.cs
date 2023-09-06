using System.Net.Http.Json;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.Tests.Extensions;
using Xunit;

namespace DressForWeather.WebAPI.Tests.IntegrationTestsForControllers;

public class ClotchTests : TestFixtureWithPreLogin
{
	public ClotchTests(
		WebApplicationFactoryForTests factory) : base(factory)
	{
	}

	[Fact]
	public async Task AddingGettingWorks()
	{
		#region adding

		var t = await Client.AddRandomClotch();
		var input = t.Item1;
		var addedId = t.Item2;

		#endregion

		#region get previously added

		using var responseFromGet = await Client.GetAsync($"/Clotch?id={addedId}");

		Assert.True(responseFromGet.IsSuccessStatusCode);

		var outputSearchResult =
			await responseFromGet.Content.ReadFromJsonAsync<OutputSearchResult<OutputClotch>>();

		Assert.NotNull(outputSearchResult);
		Assert.True(outputSearchResult.IsFound);

		Assert.Equal(addedId, outputSearchResult.Entity.Id);
		Assert.True(input.ClotchParametersIds.All(id => outputSearchResult.Entity.ClotchParametersIds.Contains(id)));
		Assert.Equal(input.Name, outputSearchResult.Entity.Name);
		Assert.Equal(input.Type, outputSearchResult.Entity.Type);

		#endregion
	}
}