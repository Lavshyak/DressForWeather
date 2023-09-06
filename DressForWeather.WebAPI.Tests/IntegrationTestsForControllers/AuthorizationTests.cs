using System.Net;
using System.Net.Http.Json;
using DressForWeather.SharedModels;
using DressForWeather.WebAPI.Tests.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DressForWeather.WebAPI.Tests.IntegrationTestsForControllers;

public class AuthorizationTests :
	IClassFixture<WebApplicationFactoryForTests>
{
	private readonly HttpClient _client;

	private readonly string _password = "1234!";
	private readonly string _userName = "TestUser" + Random.Shared.Next(100000);

	public AuthorizationTests(
		WebApplicationFactoryForTests factory)
	{
		_client = factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});
	}

	[Fact]
	public async Task RegisterLogoutLoginLogoutWorks()
	{
		#region register

		using var requestContentRegister =
			JsonContent.Create(new RegisterParameters {Password = _password, UserName = _userName});
		using var responseRegister = await _client.PostAsync("/Authorize/Register",
			requestContentRegister);

		Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);

		var coockiesFromRegister = Assert.Single(responseRegister.Headers, h =>
			h.Key == "Set-Cookie" && h.Value.Any());

		_client.DefaultRequestHeaders.Add(coockiesFromRegister.Key, coockiesFromRegister.Value);

		#endregion

		await _client.Logout();

		#region login

		using var requestContentLogin =
			JsonContent.Create(new LoginParameters {Password = _password, UserName = _userName, RememberMe = true});
		using var responseLogin = await _client.PostAsync("/Authorize/Login",
			requestContentLogin);

		Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);

		var coockies = Assert.Single(responseLogin.Headers, h =>
			h.Key == "Set-Cookie" && h.Value.Any());

		_client.DefaultRequestHeaders.Add(coockies.Key, coockies.Value);

		#endregion

		await _client.Logout();
	}
}