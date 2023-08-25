namespace DressForWeather.WebAPI;

internal static partial class Program
{
	private static string GetConnectionStringForDbContext(this ConfigurationManager configurationManager,
		string providerName, string contextName, string? defaultString = null)
	{
		return configurationManager.GetConnectionString($"{providerName}{contextName}Connection")
		       ?? defaultString
		       ?? throw new Exception(
			       $"connection string for {providerName} : {contextName} does not exists in configuration");
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