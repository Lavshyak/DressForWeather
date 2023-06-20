using System.ComponentModel.DataAnnotations;

namespace DressForWeather.Models.EFCoreModels;

public class Pair<TFirst,TSecond>
{
	public int Id { get; set; }
	
	public required TFirst First { get; set; }
	
	public required TSecond Second { get; set; }
}