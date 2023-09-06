using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace DressForWeather.WebAPI.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class WebApplicationFactoryForTests : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment(Environments.Development);
	}
}