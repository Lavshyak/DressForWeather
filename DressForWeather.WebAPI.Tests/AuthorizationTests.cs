using System.Net;
using System.Net.Http.Json;
using DressForWeather.SharedModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DressForWeather.WebAPI.Tests;

public class AuthorizationTests :
	IClassFixture<CustomWebApplicationFactory>
{
	private readonly HttpClient _client;

	private readonly CustomWebApplicationFactory
		_factory;

	public AuthorizationTests(
		CustomWebApplicationFactory factory)
	{
		_factory = factory;
		_client = factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});
	}

	private const string Password = "1234!";
	private const string UserName = "TestUser123";
	
	[Fact]
	public async Task RegisterLogoutWorks()
	{
		using var requestContentRegister =
			JsonContent.Create(new RegisterParameters() {Password = Password, UserName = UserName});
		using var responseRegister = await _client.PostAsync("/Authorize/Register",
			requestContentRegister);

		Assert.Equal(HttpStatusCode.OK, responseRegister.StatusCode);

		var coockies = Assert.Single(responseRegister.Headers, h =>
			h.Key == "Set-Cookie" && h.Value.Any());

		_client.DefaultRequestHeaders.Add(coockies.Key, coockies.Value);
		using var responseLogout = await _client.GetAsync("Authorize/Logout");
		
		Assert.Equal(HttpStatusCode.OK, responseLogout.StatusCode);

		_client.DefaultRequestHeaders.Remove(coockies.Key);
	}

	[Fact]
	public async Task LoginLogoutWorks()
	{
		using var requestContentLogin =
			JsonContent.Create(new LoginParameters() {Password = Password, UserName = UserName, RememberMe = true});
		using var responseLogin = await _client.PostAsync("/Authorize/Login",
			requestContentLogin);
		
		Assert.Equal(HttpStatusCode.OK, responseLogin.StatusCode);
		
		var coockies = Assert.Single(responseLogin.Headers, h =>
			h.Key == "Set-Cookie" && h.Value.Any());
		
		_client.DefaultRequestHeaders.Add(coockies.Key, coockies.Value);
		
		using var responseLogout = await _client.PostAsync("/Authorize/Login",
			requestContentLogin);
		
		Assert.Equal(HttpStatusCode.OK, responseLogout.StatusCode);
		
		_client.DefaultRequestHeaders.Remove(coockies.Key);
		
	}
}