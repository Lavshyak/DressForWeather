using System.Net.Http.Json;
using DressForWeather.SharedModels;

namespace ConsoleTestClient;

internal static class Program
{
	private static HttpClient _httpClient = new HttpClient();
	private static string _baseUrl = "https://localhost:44371";

	private static async Task Main()
	{
		await Register();
	}

	public static async Task Register()
	{
		var regParams = new RegisterParameters()
		{
			UserName = "Test1",
			Password = "1234",
			PasswordConfirm = "1234"
		};

		HttpResponseMessage httpResponseMessage;

		try
		{
			httpResponseMessage =
				await _httpClient.PostAsync(new Uri($"{_baseUrl}/Authorize/Register"), JsonContent.Create(regParams));
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			throw new Exception("IsSuccessStatusCode=false");
		}
		
	}
}