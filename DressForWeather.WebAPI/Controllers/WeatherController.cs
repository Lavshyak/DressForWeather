using DressForWeather.SharedModels.Inputs;
using DressForWeather.SharedModels.Outputs;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[Authorize]
public class WeatherController : ControllerBaseWithRouteToController
{
	private readonly IMainDbContext _dbContext;

	public WeatherController(ILogger<WeatherController> logger, IMainDbContext db)
	{
		_dbContext = db;
	}

	[HttpPost]
	[ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
	public async Task<long> Set(InputWeatherState inputWeatherState)
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
	[ProducesResponseType(typeof(OutputSearchResult<WeatherState>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<WeatherState>> Get(WeatherState state)
	{
		var weather = await _dbContext.WeatherStates.FirstOrDefaultAsync(c => c.Id == state.Id);
		
		return new OutputSearchResult<WeatherState>(weather);
	}
}