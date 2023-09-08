namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class ClothParameterPair
{
	public long Id { get; set; }

	public required string Key { get; set; }

	public required string Value { get; set; }
}