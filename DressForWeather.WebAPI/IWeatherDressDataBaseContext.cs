using DressForWeather.Models;
using DressForWeather.Models.EFCoreModels;

namespace DressForWeather.WebAPI;

public interface IWeatherDressDataBaseContext
{
	public IQueryable<Info> Info { get; }
}