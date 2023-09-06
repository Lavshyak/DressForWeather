using AutoMapper;
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
	private readonly MainDbContext _dbContext;
	private readonly IMapper _mapper;

	public WeatherController(ILogger<WeatherController> logger, MainDbContext db, IMapper mapper)
	{
		_dbContext = db;
		_mapper = mapper;
	}

	/// <summary>
	///     Добавить информацию о погоде
	/// </summary>
	/// <param name="inputWeatherState">информация о погоде</param>
	/// <returns>Идентификатор</returns>
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

		await _dbContext.SaveChangesAsync();

		return weatherState.Entity.Id;
	}

	/// <summary>
	///     Получить информацию о погоде
	/// </summary>
	/// <param name="id">Идентификатор информации о погоде</param>
	/// <returns>Результат поиска</returns>
	[HttpGet]
	[ProducesResponseType(typeof(OutputSearchResult<WeatherState>), StatusCodes.Status200OK)]
	public async Task<OutputSearchResult<OutputWeatherState>> Get([FromQuery] long id)
	{
		var weather = await _dbContext.WeatherStates.FirstOrDefaultAsync(c => c.Id == id);

		OutputWeatherState? outputWeather = null;

		if (weather != null)
			outputWeather = new OutputWeatherState
			{
				HowSunny = weather.HowSunny, Humidity = weather.Humidity, Id = weather.Id,
				TemperatureCelsius = weather.TemperatureCelsius,
				WindDirection = weather.WindDirection, WindSpeedMps = weather.WindSpeedMps
			};

		return new OutputSearchResult<OutputWeatherState>(outputWeather);
	}
}