using System.ComponentModel.DataAnnotations;

namespace DressForWeather.SharedModels.Outputs;

public class OutputDressReport
{
	public long Id { get; set; }

	/// <summary>
	///     одежда
	/// </summary>
	public required List<long> ClothesIds { get; set; }

	/// <summary>
	///     пользователь, который оставляет этот отзыв
	/// </summary>
	public required long UserReporterId { get; set; }

	/// <summary>
	///     состояние погоды
	/// </summary>
	public required OutputWeatherState OutputWeatherState { get; set; }

	/// <summary>
	///     -1 - смертельно холодно, +1 смертельно жарко. 0 - идеально
	/// </summary>
	[Range(-1, 1)]
	public float Feeling { get; set; }
}