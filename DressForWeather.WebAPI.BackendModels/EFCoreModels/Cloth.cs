namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class Cloth
{
	public long Id { get; set; }

	public required string Type { get; set; }

	public required string Name { get; set; }

	public required List<ClothParameterPair> ClotchParameters { get; set; } = new();
}