namespace DressForWeather.Models.EFCoreModels;

public class ClothesSet
{
	public long Id { get; set; }
	public required List<Clotch> Clotches { get; set; } = new();
	public required User Creator { get; set; }
}