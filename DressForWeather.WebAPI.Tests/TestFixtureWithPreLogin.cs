using DressForWeather.WebAPI.Tests.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DressForWeather.WebAPI.Tests;

public class TestFixtureWithPreLogin : IClassFixture<WebApplicationFactoryForTests>, IAsyncLifetime
{
	protected readonly HttpClient Client;

	protected TestFixtureWithPreLogin(WebApplicationFactoryForTests factory)
	{
		Client = factory.CreateClient(new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false
		});
	}

	//called from xunit
	public async Task InitializeAsync()
	{
		await Client.LoginOrRegisterAsync();
	}

	//called from xunit
	public async Task DisposeAsync()
	{
		await Client.Logout();
		//Client сам диспозится откуда-то
	}
}