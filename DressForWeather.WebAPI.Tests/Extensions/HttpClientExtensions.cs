using System.Net;
using System.Net.Http.Json;
using DressForWeather.SharedModels;
using DressForWeather.SharedModels.Inputs;
using Xunit;

namespace DressForWeather.WebAPI.Tests.Extensions;

public static class HttpClientExtensions
{
	public static async Task LoginOrRegisterAsync(this HttpClient httpClient, string login = "TestUser12345wwtw",
		string password = "1234!ertrt")
	{
		using var requestContentLogin =
			JsonContent.Create(new LoginParameters {UserName = login, Password = password, RememberMe = true}
			);
		using var responseLogin = await httpClient.PostAsync("/Authorize/Login",
			requestContentLogin);

		if (responseLogin.StatusCode != HttpStatusCode.OK) await httpClient.RegisterAsync(login, password);

		KeyValuePair<string, IEnumerable<string>>? coockies =
			responseLogin.Headers.SingleOrDefault(
				h =>
					h.Key == "Set-Cookie" && h.Value.Any()
			);

		if (coockies.HasValue && !string.IsNullOrWhiteSpace(coockies.Value.Key) && coockies.Value.Value.Any())
			httpClient.DefaultRequestHeaders.Add(coockies.Value.Key, coockies.Value.Value);
		else
			await httpClient.RegisterAsync(login, password);
	}

	public static async Task RegisterAsync(this HttpClient httpClient, string login = "TestUser12345wwtw",
		string password = "1234!ertrt")
	{
		using var requestContentRegister =
			JsonContent.Create(new RegisterParameters {UserName = login, Password = password});

		using var responseRegister = await httpClient.PostAsync("/Authorize/Register",
			requestContentRegister);

		Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);

		var coockiesFromRegister =
			Assert.Single(responseRegister.Headers,
				h =>
					h.Key == "Set-Cookie" && h.Value.Any()
			);

		httpClient.DefaultRequestHeaders.Add(coockiesFromRegister.Key, coockiesFromRegister.Value);
	}

	public static async Task Logout(this HttpClient httpClient)
	{
		using var responseLogout = await httpClient.GetAsync("Authorize/Logout");
		Assert.Equal(HttpStatusCode.OK, responseLogout.StatusCode);

		httpClient.DefaultRequestHeaders.Remove("Set-Cookie");
	}

	public static async Task<Tuple<InputClotchParameterPair, long>> AddRandomPair(this HttpClient httpClient)
	{
		var input = new InputClotchParameterPair
			{Key = Random.Shared.NextInt64().ToString(), Value = Random.Shared.NextInt64().ToString()};

		using var responseFromAdd = await httpClient.PostAsJsonAsync("/ClotchParameterPair",
			input);
		Assert.True(responseFromAdd.IsSuccessStatusCode);

		var addedId = await responseFromAdd.Content.ReadFromJsonAsync<long?>();
		Assert.True(addedId.HasValue);

		return Tuple.Create(input, addedId.Value);
	}

	public static async Task<Tuple<InputClotch, long>> AddRandomClotch(this HttpClient httpClient)
	{
		var inputPairs =
			new[] {await httpClient.AddRandomPair(), await httpClient.AddRandomPair()};

		var input = new InputClotch
		{
			ClotchParametersIds = inputPairs.Select(t => t.Item2),
			Name = Random.Shared.NextInt64().ToString(),
			Type = Random.Shared.NextInt64().ToString()
		};

		using var responseFromAdd = await httpClient.PostAsJsonAsync("/Clotch",
			input);
		Assert.True(responseFromAdd.IsSuccessStatusCode);

		var addedId = await responseFromAdd.Content.ReadFromJsonAsync<long?>();
		Assert.True(addedId.HasValue);

		return Tuple.Create(input, addedId.Value);
	}

	public static async Task<Tuple<InputWeatherState, long>> AddRandomWeatherState(this HttpClient httpClient)
	{
		var inputPairs =
			new[] {await httpClient.AddRandomPair(), await httpClient.AddRandomPair()};

		var input = new InputWeatherState
		{
			HowSunny = Random.Shared.NextSingle(),
			Humidity = Random.Shared.NextSingle(),
			TemperatureCelsius = Random.Shared.NextSingle() + Random.Shared.Next(-30, 30),
			WindDirection = WindDirection.South,
			WindSpeedMps = Random.Shared.NextSingle() + Random.Shared.Next(0, 100)
		};

		using var responseFromAdd = await httpClient.PostAsJsonAsync("/Weather",
			input);
		Assert.True(responseFromAdd.IsSuccessStatusCode);

		var addedId = await responseFromAdd.Content.ReadFromJsonAsync<long?>();
		Assert.True(addedId.HasValue);

		return Tuple.Create(input, addedId.Value);
	}

	public static async Task<Tuple<InputDressReport, long>> AddRandomDressReport(this HttpClient httpClient)
	{
		var inputclothes =
			new[] {await httpClient.AddRandomClotch(), await httpClient.AddRandomClotch()};

		var inputWeather = await httpClient.AddRandomWeatherState();
		var input = new InputDressReport
		{
			ClothIds = inputclothes.Select(t => t.Item2),
			Feeling = Random.Shared.NextSingle() - Random.Shared.NextSingle(),
			WeatherStateId = inputWeather.Item2
		};

		using var responseFromAdd = await httpClient.PostAsJsonAsync("/DressReport",
			input);
		Assert.True(responseFromAdd.IsSuccessStatusCode);

		var addedId = await responseFromAdd.Content.ReadFromJsonAsync<long?>();
		Assert.True(addedId.HasValue);

		return Tuple.Create(input, addedId.Value);
	}
}