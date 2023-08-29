using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;

namespace DressForWeather.WebAPI;

public static partial class Program
{
	public static async Task Main(string[] args)
	{
		var builder = new WebHostBuilder().UseStartup<Startup>();
		var app = builder.Build();
		await app.RunAsync();
		
		/*var builder = WebApplication.CreateBuilder(args);
		var startup = new Startup(builder.Configuration);
		startup.ConfigureServices(builder.Services);
		var app = builder.Build();
		startup.Configure(app, app.Environment);
		app.Run();*/

	}

	

	/*private static async Task ConfigureApp(this WebApplication app)
	{
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		await app.Services.SynchronizeIdentityRoles();
	}*/

	
}