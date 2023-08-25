using System.ComponentModel.DataAnnotations;
using DressForWeather.WebAPI.BackendModels;

namespace DressForWeather.SharedModels.Entities;

public class AddWeatherStateModel
{
	public float TemperatureCelsius { get; set; }

	/// <summary>
	///     влажность
	/// </summary>
	[Range(0, 1, ErrorMessage = "Must be between 0 and 1")]
	public float Humidity { get; set; }

	/// <summary>
	///     скорость ветра в м/с
	/// </summary>
	[Range(0, float.MaxValue)]
	public float WindSpeedMps { get; set; }

	/// <summary>
	///     направление ветра
	/// </summary>
	public WindDirection WindDirection { get; set; }

	/// <summary>
	///     На сколько солнечно от 0 до 1, где 0 - 0%, 1 - 100%
	/// </summary>
	[Range(0, 1, ErrorMessage = "Must be between 0 and 1")]
	public float HowSunny { get; set; }
}