namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class ClotchParameterPair
{
	public int Id { get; set; }
	
	public required string First { get; set; }
	
	public required string Second { get; set; }
}