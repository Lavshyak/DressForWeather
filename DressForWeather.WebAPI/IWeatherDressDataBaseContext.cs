using DressForWeather.Models;

namespace WeatherDress.WebAPI;

public interface IWeatherDressDataBaseContext
{
	public IQueryable<Info> Info { get; }
}