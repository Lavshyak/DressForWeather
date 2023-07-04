namespace DressForWeather.WebAPI;

internal static partial class Program
{
	private static class DbProviders
	{
		public const string Postgre = "Postgre";
	}

	private static string GetConnectionStringForDbContext(this ConfigurationManager configurationManager,
		string providerName, string contextName, string? defaultString = null)
		=> configurationManager.GetConnectionString(($"{providerName}{contextName}Connection"))
		   ?? defaultString
		   ?? throw new Exception(
			   $"connection string for {providerName} : {contextName} does not exists in configuration");

	private static string GetDbProviderNameFor(this ConfigurationManager configurationManager, string contextName,
		string? defaultName = null)
		=> configurationManager.GetSection("DbProviders").GetValue(contextName, defaultName)
		   ?? throw new Exception($"provider name for {contextName} does not exists in configuration");

	private static string GetMigrationsAssemblyNameFor(string dbProviderName)
		=> $"DressForWeather.WebAPI.{dbProviderName}Migrations";
}