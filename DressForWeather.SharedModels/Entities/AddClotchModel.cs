namespace DressForWeather.SharedModels.Entities;

public class AddClotchModel
{
	public required string Type { get; set; }

	public required string Name { get; set; }

	public required List<int> ClotchParametersIds { get; set; } = new();
}