namespace DressForWeather.SharedModels.Outputs;

public class OutputClotch
{
	public required long Id { get; set; }

	public required string Type { get; set; }

	public required string Name { get; set; }

	public required List<OutputClotchParameterPair> ClotchParameters { get; set; } = new();
}