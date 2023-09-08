using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI;

public static class StartupExtensions
{
	//названия ролей, которе нужны серверу для авторизации. Пока используется только User
	private static readonly string[] RequiredRoleNames = {"Admin", "User"};

	private static void AddManualAuthorization(this IServiceCollection services)
	{
		//Identity реализует алгоритмы авторизации
		services.AddIdentity<User, IdentityRole<long>>()
			.AddEntityFrameworkStores<MainDbContext>();

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

	public static void ConfigureApp(this WebApplication app)
	{
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			//"фронт" в браузере. в основном предназначен для интеграционного ручного тестирования API
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		//добавить методы из контроллеров в API
		app.MapControllers();

		if (app.Environment.IsDevelopment())
		{
			//app.Services.ClearDataBase().GetAwaiter().GetResult();
		}

		app.Services.SynchronizeIdentityRoles().GetAwaiter().GetResult();
	}

	//не используется
	private static async Task ClearDataBase(this IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var mainDbContext = scope.ServiceProvider.GetService<MainDbContext>() ?? throw new Exception();
		var contextType = typeof(MainDbContext);
		var props = contextType.GetProperties().Where(p => p.PropertyType == typeof(DbSet<>));
		foreach (var prop in props)
		{
			var task = (prop.GetValue(mainDbContext) as DbSet<object>)?.ExecuteDeleteAsync();
			if (task is not null)
				await task;
		}
	}

	public static void ConfigureServices(this WebApplicationBuilder builder)
	{
		ConfigureServices(builder.Services, builder.Configuration);
	}


	private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
	{
		// Add services to the container.
		services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		//добавляет контекст базы данных, который будет использоваться методами API для добавления/извлечения из базы данных
		services.AddDbContext<MainDbContext>(
			options =>
			{
				var dbProvider =
					configuration.GetDbProviderNameFor(MainDbContext.ContextName, DbProviders.Postgre);
				_ = dbProvider
					switch
					{
						//для добавления миграции:
						/*
						 "C:\Program Files\dotnet\dotnet.exe" ef migrations add --project
						 DressForWeather.WebAPI.PostgreMigrations\DressForWeather.WebAPI.PostgreMigrations.csproj
						 --startup-project DressForWeather.WebAPI\DressForWeather.WebAPI.csproj
						 --context DressForWeather.WebAPI.DbContexts.MainDbContext --configuration Debug MIGRATION_NAME
						 --output-dir Migrations
						 */

						//для приминения миграции:
						/*
							для основной бд:
						+R - Release
						+P - Production
							для тестовой бд:
						+R - Debug
						+P - Development
					*/
						/*
						 "C:\Program Files\dotnet\dotnet.exe" ef database update --project
						 DressForWeather.WebAPI.PostgreMigrations\DressForWeather.WebAPI.PostgreMigrations.csproj
						 --startup-project DressForWeather.WebAPI\DressForWeather.WebAPI.csproj
						 --context DressForWeather.WebAPI.DbContexts.MainDbContext
						 --configuration +R MIGRATION_NAME
						 -- --environment +P
						 */
						DbProviders.Postgre => options.UseNpgsql(
							configuration.GetConnectionStringForDbContext(dbProvider,
								MainDbContext.ContextName),
							x => x.MigrationsAssembly(GetMigrationsAssemblyNameFor(dbProvider))),
						//DressForWeather.WebAPI.PostgreMigrations

						//если перейдем на другую базу данных, то нужно сделать действия, аналогичные строке выше

						_ => throw new Exception($"Unsupported provider: {dbProvider}")
					};
			});

		//services.AddScoped<MainDbContext>();

		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();

		services.AddAuthorization();
		services.AddManualAuthorization();

		//добавление автоматического конвертировщика моделей в сервисы из AppMappingProfile.cs
		services.AddAutoMapper(typeof(AppMappingProfile));
	}

	/// <summary>
	/// Добавляет в базу данных роли пользователей, которые нужны для функционирования сервера, если их нет.
	/// </summary>
	/// <param name="serviceProvider"></param>
	/// <exception cref="Exception"></exception>
	private static async Task SynchronizeIdentityRoles(this IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<long>>>() ?? throw new Exception();
		var existRoles = roleManager.Roles.ToArray();
		var missingRoleNames = RequiredRoleNames.Where(rs => existRoles.All(r => r.Name != rs));
		foreach (var roleName in missingRoleNames) await roleManager.CreateAsync(new IdentityRole<long>(roleName));
	}

	private static string GetConnectionStringForDbContext(this ConfigurationManager configurationManager,
		string providerName, string contextName, string? defaultString = null)
	{
		var connectionString = configurationManager.GetConnectionString($"{providerName}{contextName}Connection")
		                       ?? defaultString
		                       ?? throw new Exception(
			                       $"connection string for {providerName} : {contextName} does not exists in configuration");
		return connectionString;
	}

	private static string GetDbProviderNameFor(this ConfigurationManager configurationManager, string contextName,
		string? defaultName = null)
	{
		return configurationManager.GetSection("DbProviders").GetValue(contextName, defaultName)
		       ?? throw new Exception($"provider name for {contextName} does not exists in configuration");
	}

	private static string GetMigrationsAssemblyNameFor(string dbProviderName)
	{
		return $"DressForWeather.WebAPI.{dbProviderName}Migrations";
	}

	private static class DbProviders
	{
		public const string Postgre = "Postgre";
	}
}