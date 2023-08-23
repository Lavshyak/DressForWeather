using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using DressForWeather.WebAPI.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

public class WeatherController : ControllerBaseDressForWeather
{
    private readonly MainDbContext _dbContext;
    public WeatherController(ILogger<WeatherController> logger, MainDbContext db) 
    {
        _dbContext = db;
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
