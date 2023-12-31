namespace DressForWeather.SharedModels.Inputs;

public class InputCloth
{
	public required string Type { get; set; }

	public required string Name { get; set; }

	public required IEnumerable<long> ClotchParametersIds { get; set; }
}