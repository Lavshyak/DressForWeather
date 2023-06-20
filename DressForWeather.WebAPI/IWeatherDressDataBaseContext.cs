using DressForWeather.Models;

namespace DressForWeather.WebAPI;

public interface IWeatherDressDataBaseContext
{
	public IQueryable<Info> Info { get; }
}