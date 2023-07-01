using DressForWeather.DbContexts;
using DressForWeather.Models.EFCoreModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI;

internal static class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.ConfigureServices();
		var app = builder.Build();
		app.ConfigureApp();
		app.Run();
	}

	private static void ConfigureServices(this WebApplicationBuilder builder)
	{
		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var dbProvider = builder.Configuration.GetSection("DbProviders").GetValue("WeatherDressContext", "PostgreSql");
		builder.Services.AddDbContext<WeatherDressDbContext>(
			options => _ = dbProvider
				switch
				{
					"PostgreSql" => options.UseNpgsql(
						builder.Configuration.GetConnectionString("PostgreSqlDressForWeather"),
						x => x.MigrationsAssembly("PostgreSqlMigrations")),
			
					_ => throw new Exception($"Unsupported provider: {dbProvider}")
				}
		);

		builder.Services.AddScoped<WeatherDressDbContext>();

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();
		builder.Services.AddAuthorization();

		builder.Services.AddManualAuthorization();
	}
	private static void AddManualAuthorization(this IServiceCollection services)
	{
		services.AddIdentity<User, IdentityRole<long>>()
			.AddEntityFrameworkStores<WeatherDressDbContext>()
			.AddDefaultTokenProviders();

		services.Configure<IdentityOptions>(options =>
		{
			// Password settings
			options.Password.RequireDigit = false;
			options.Password.RequiredLength = 4;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequireLowercase = false;

			// Lockout settings
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
			options.Lockout.MaxFailedAccessAttempts = 10;
			options.Lockout.AllowedForNewUsers = true;

			// User settings
			options.User.RequireUniqueEmail = false;
		});
		
		services.ConfigureApplicationCookie(options =>
		{
			options.Cookie.HttpOnly = true;
			options.Events.OnRedirectToLogin = context =>
			{
				context.Response.StatusCode = 401;
				return Task.CompletedTask;
			};
		});
	}
	private static void ConfigureApp(this WebApplication app)
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
	}
	
}