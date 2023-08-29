namespace DressForWeather.SharedModels.Inputs;

public class InputClotch
{
	public required string Type { get; set; }

	public required string Name { get; set; }

	public required List<long> ClotchParametersIds { get; set; } = new();
}