namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class WeatherState
{
	public long Id { get; set; }
	
	public float TemperatureCelsius { get; set; }
	
	/// <summary>
	/// влажность
	/// </summary>
	public float Humidity { get; set; }
	
	/// <summary>
	/// скорость ветра в м/с
	/// </summary>
	public float WindSpeedMps { get; set; }
	
	/// <summary>
	/// направление ветра
	/// </summary>
	public WindDirection WindDirection { get; set; }

	/// <summary>
	/// На сколько солнечно от 0 до 1, где 0 - 0%, 1 - 100%
	/// </summary>
	public float HowSunny { get; set; }
}