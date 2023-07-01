namespace DressForWeather.Models.EFCoreModels;

public class DressReport
{
	public long Id { get; set; }
	
	/// <summary>
	/// одежда
	/// </summary>
	public required ClothesSet ClothesSet { get; set; }
	
	/// <summary>
	/// пользователь, который оставляет этот отзыв
	/// </summary>
	public required User UserReporter { get; set; }
	
	/// <summary>
	/// состояние погоды
	/// </summary>
	public required WeatherState WeatherState { get; set; }
	
	/// <summary>
	/// предположительно так: -1 - супермегахолодно умер, +1 супермегажарко умер. 0 - идеально
	/// </summary>
	public float Feeling { get; set; }
}