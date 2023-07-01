using System.ComponentModel.DataAnnotations;

namespace DressForWeather.Models.EFCoreModels;

public class ClotchParameterPair
{
	public int Id { get; set; }
	
	public required string First { get; set; }
	
	public required string Second { get; set; }
}