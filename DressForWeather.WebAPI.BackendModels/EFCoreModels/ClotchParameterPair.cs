namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class ClotchParameterPair
{
	public int Id { get; set; }

	public required string Key { get; set; }

	public required string Value { get; set; }
}