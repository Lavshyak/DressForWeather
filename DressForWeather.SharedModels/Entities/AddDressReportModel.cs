using System.ComponentModel.DataAnnotations;

namespace DressForWeather.SharedModels.Entities;

public class AddDressReportModel
{
	/// <summary>
	///     одежда
	/// </summary>
	public required List<long> ClothIds { get; set; }

	/// <summary>
	///     id состояния погоды
	/// </summary>
	public required long WeatherStateId { get; set; }

	/// <summary>
	///     -1 - смертельно холодно, +1 смертельно жарко. 0 - идеально
	/// </summary>
	[Range(-1, 1)]
	public float Feeling { get; set; }
}