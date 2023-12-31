namespace DressForWeather.SharedModels.Outputs;

public class OutputCloth
{
	public required long Id { get; set; }

	public required string Type { get; set; }

	public required string Name { get; set; }

	public required IEnumerable<long> ClotchParametersIds { get; set; }
}