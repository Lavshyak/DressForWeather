using DressForWeather.SharedModels;
using DressForWeather.WebAPI.BackendModels.EFCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressForWeather.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : Controller
{
    private readonly WeatherDressDbContext _dbContext;
    private readonly ILogger<WeatherController> _logger;
    public WeatherController(ILogger<WeatherController> logger, WeatherDressDbContext db) 
    {
        _logger = logger;
        _dbContext = db;
    }
    [HttpGet]
    public async Task<WeatherState> GetWeatherById(WeatherState state) 
    {
        if (state == null)
            throw new Exception("WeatherState error");
        var weather = await _dbContext.WeatherStates.FirstOrDefaultAsync(c => c.Id == state.Id);
        return weather;
    }
}
