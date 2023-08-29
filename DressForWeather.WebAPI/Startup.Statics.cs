using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Identity;

namespace DressForWeather.WebAPI;

public partial class Startup
{

private static readonly string[] RequiredRoleNames = {"Admin", "User"};
	
	public static void AddManualAuthorization(IServiceCollection services)
	{
		services.AddIdentity<User, IdentityRole<long>>()
			.AddEntityFrameworkStores<MainDbContext>();
		//.AddRoles<IdentityRole>(); //попытался сделать это чтоб авторизация работала (работает без этого теперь),
		//но ошибка рантайма, нужен еще какой-то сервис. может потом пригодится

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
	
	public static string GetConnectionStringForDbContext(IConfiguration configuration,
		string providerName, string contextName, string? defaultString = null)
	{
		return configuration.GetConnectionString($"{providerName}{contextName}Connection")
		       ?? defaultString
		       ?? throw new Exception(
			       $"connection string for {providerName} : {contextName} does not exists in configuration");
	}

	public static string GetDbProviderNameFor(IConfiguration configuration, string contextName,
		string? defaultName = null)
	{
		return configuration.GetSection("DbProviders").GetValue(contextName, defaultName)
		       ?? throw new Exception($"provider name for {contextName} does not exists in configuration");
	}

	public static string GetMigrationsAssemblyNameFor(string dbProviderName)
	{
		return $"DressForWeather.WebAPI.{dbProviderName}Migrations";
	}

	private static class DbProviders
	{
		public const string Postgre = "Postgre";
	}
	
	public static async Task SynchronizeIdentityRoles(IServiceProvider serviceProvider)
	{
		using var scope = serviceProvider.CreateScope();
		var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<long>>>() ?? throw new Exception();
		var existRoles = roleManager.Roles.ToArray();
		var missingRoleNames = RequiredRoleNames.Where(rs => existRoles.All(r => r.Name != rs));
		await Task.WhenAll(
			missingRoleNames.Select(roleName => roleManager.CreateAsync(new IdentityRole<long>(roleName))));
	}
	}