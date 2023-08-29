using DressForWeather.SharedModels.Inputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class WeatherController : ControllerBaseDressForWeather
{
	private readonly MainDbContext _dbContext;

	public WeatherController(ILogger<WeatherController> logger, MainDbContext db)
	{
		_dbContext = db;
	}

	[HttpPost]
	public async Task<long> AddWeatherState(InputWeatherState inputWeatherState)
	{
		var weatherState = await _dbContext.WeatherStates.AddAsync(new WeatherState
		{
			HowSunny = inputWeatherState.HowSunny,
			Humidity = inputWeatherState.Humidity,
			TemperatureCelsius = inputWeatherState.TemperatureCelsius,
			WindDirection = inputWeatherState.WindDirection,
			WindSpeedMps = inputWeatherState.WindSpeedMps
		});
		return weatherState.Entity.Id;
	}

	[HttpGet]
	public async Task<WeatherState?> GetWeatherById(WeatherState state)
	{
		if (state == null)
			throw new Exception("WeatherState error");
		var weather = await _dbContext.WeatherStates.FirstOrDefaultAsync(c => c.Id == state.Id);
		return weather;
	}
}