using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI;

internal static partial class Program
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

		builder.Services.AddDbContext<MainDbContext>(
			options =>
			{
				var dbProvider = builder.Configuration.GetDbProviderNameFor(MainDbContext.ContextName, DbProviders.Postgre);
				_ = dbProvider
					switch
					{
						//сделано: dotnet ef migrations add InitialCreate --project ../DressForWeather.WebAPI.PostgreMigrations -- --provider Postgre
						//потом: dotnet ef database update
						DbProviders.Postgre => options.UseNpgsql(
							builder.Configuration.GetConnectionStringForDbContext(dbProvider, MainDbContext.ContextName),
							x => x.MigrationsAssembly(GetMigrationsAssemblyNameFor(dbProvider))),
						
						//если перейдем на другую базу данных, то нужно сделать действия, аналогичные строке выше

						_ => throw new Exception($"Unsupported provider: {dbProvider}")
					};
			});

		builder.Services.AddScoped<MainDbContext>();

		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();
		builder.Services.AddAuthorization();

		builder.Services.AddManualAuthorization();
	}

	private static void AddManualAuthorization(this IServiceCollection services)
	{
		services.AddIdentity<User, IdentityRole<long>>()
			.AddEntityFrameworkStores<MainDbContext>();			
		//.AddRoles<IdentityRole>(); //попытался сделать это чтоб авторизация работала,
       //но ошибка рантайма, нужен еще какой-то сервис

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