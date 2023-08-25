using System.ComponentModel.DataAnnotations;

namespace DressForWeather.WebAPI.BackendModels.EFCoreModels;

public class DressReport
{
	public long Id { get; set; }

	/// <summary>
	///     одежда
	/// </summary>
	public required List<Clotch> Clothes { get; set; }

	/// <summary>
	///     пользователь, который оставляет этот отзыв
	/// </summary>
	public required User UserReporter { get; set; }

	/// <summary>
	///     состояние погоды
	/// </summary>
	public required WeatherState WeatherState { get; set; }

	/// <summary>
	///     -1 - смертельно холодно, +1 смертельно жарко. 0 - идеально
	/// </summary>
	[Range(-1, 1)]
	public float Feeling { get; set; }
}