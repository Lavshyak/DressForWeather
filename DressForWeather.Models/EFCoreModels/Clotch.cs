using System.ComponentModel.DataAnnotations;

namespace DressForWeather.Models.EFCoreModels;

public class Clotch
{
	public long Id { get; set; }
	
	public required string Type { get; set; }
	
	public required string Name { get; set; }
	
	public required List<ClotchParameterPair> ClotchParameters { get; set; } = new();
}